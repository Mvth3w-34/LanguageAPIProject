using LanguageProjectBackend.Data;
using LanguageProjectBackend.Dtos;
using LanguageProjectBackend.Models;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace LanguageProjectBackend.Services
{
    //This class will be used send emails to the client.
    public class SendGridService
    {
        private readonly IUserRepo _userRepository;
        private readonly IWordRepo _wordRepository;
        private readonly IUserWordRepo _userWordRepository;
        private string _apiKey = Environment.GetEnvironmentVariable("EMAIL_API_KEY");
        private string _sender = Environment.GetEnvironmentVariable("EMAIL_SENDER");
        private string templateId = Environment.GetEnvironmentVariable("TEMPLATE_ID");


        public SendGridService(IUserRepo userRepo, IUserWordRepo uWord, IWordRepo wordRepo)
        {
            _userRepository = userRepo;
            _wordRepository = wordRepo;
            _userWordRepository = uWord;
        }


        //This method will send an email to user based on the frequency they previously selected.
        public void SendNewWordEmail(string emailFrequency)
        {
            string? translation;

            Translator translator = new Translator();

            IEnumerable<User> users = _userRepository.GetUserByEmailFrequency(emailFrequency); //List of recipients

            foreach (User user in users)
            {

                NewWord newWord = _wordRepository.GetNewWord(user.Id); //Get a new word for the user.

                var content = ""; //Email content

                //Check if a new word is returned.
                if (newWord != null)
                {
                    //Check if a translation exists in the database 
                    if (_wordRepository.GetTranslation(newWord, user.LanguagePreference) != null)
                    {
                        translation = _wordRepository.GetTranslation(newWord, user.LanguagePreference);
                    }
                    //Translate the word if it doesn't. 
                    else
                    {
                        translation = translator.TranslateWord(newWord.Word, user.LanguagePreference).ToLower();

                        //Add the translation to the db.
                        switch (user.LanguagePreference)
                        {
                        case "Swahili":
                            newWord.Swahili = translation;
                            break;
                        case "Arabic":
                            newWord.Arabic = translation;
                            break;
                        default:
                            newWord.French = translation;
                            break;
                        }

                        _wordRepository.SaveChanges(); //Save the translation.

                    }

                    //Add the new word to user's list
                    UserWord userWord = new UserWord()
                    {
                        UserId = user.Id,
                        WordId = newWord.Id,
                    };

                    _userWordRepository.CreateUserWord(userWord);
                    _userWordRepository.SaveChanges();


                    //Compose the content for the email 
                    content = $"Hello {user.FirstName}, \n Your {user.LanguagePreference} word of the day is {translation} which in english means {newWord.Word}.";

                }
                else
                {
                    //Compose an email letting the user know that they have completed the current dictionary set.
                    content = $"Hello {user.FirstName}, \n We are all out of words for you at the moment ";
                }

                //Components needed for the email.

                SendEmail(user.Email, "New Vocabulary Word", content);


            }

        }

        //This method will acts a general format for sending an email.
        public void SendEmail(string email, string subject, string emailContent)
        {
            if (_apiKey.IsNullOrEmpty())
            {
                throw new Exception("The api key is empty");
            }
            var client = new SendGridClient(_apiKey);
            var from = new EmailAddress(_sender, "VocabHelper");
            var to = new EmailAddress(email);
            var msg = new SendGridMessage
            {
                TemplateId = templateId,
                From = from
            };

            msg.AddTo(to);

            //Overides placeholders in the SendGrid template
            msg.SetTemplateData(new { Subject = subject, Content = emailContent });

            var response = client.SendEmailAsync(msg);
        }


        //This method will be used to fetch all of the unsubscribed users.
        public async Task<List<GlobalUnsubscriberDto>?> FetchGlobalUnsubscribers()
        {
            var client = new SendGridClient(_apiKey);
            var response = await client.RequestAsync(
                        method: SendGridClient.Method.GET,
                        urlPath: "supression/global_unsubscribe"
                ).ConfigureAwait(false);

            //Check if the request went through
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new Exception("No users found");
            }
            var unsubscribedUsers = JsonConvert.DeserializeObject<List<GlobalUnsubscriberDto>>(response.ToString());

            return unsubscribedUsers;

        }



    }







}
