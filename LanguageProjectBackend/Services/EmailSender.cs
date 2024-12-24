using LanguageProjectBackend.Data;
using LanguageProjectBackend.Models;
using Microsoft.Identity.Client;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.ComponentModel.DataAnnotations;

namespace LanguageProjectBackend.Services
{
    //This class will be used send emails to the client.
    public class EmailSender
    {
        private readonly IUserRepo _userRepository;
        private readonly IWordRepo _wordRepository;
        private readonly IUserWordRepo _userWordRepository;

        public EmailSender(IUserRepo userRepo, IUserWordRepo uWord, IWordRepo wordRepo)
        {
            _userRepository=userRepo;
            _wordRepository=wordRepo;
            _userWordRepository=uWord;
        }
        
        //This method will send an email to user based on the frequency they previously selected.
        public async Task SendEmailAsync(string emailFrequency) 
        {
            string? translation;

            Translator translator = new Translator();
            
            IEnumerable<User> users = _userRepository.GetUserByEmailFrequency(emailFrequency); //List of recipients

            foreach (User user in users) {

                NewWord newWord = _wordRepository.GetNewWord(user.Id); //Get a new word for the user.
                
                var plainTextContext = ""; //Email content

                if (newWord != null) //Check if the new word exists.
                {
                    //Check if a translation exists in the database 
                    if (_wordRepository.GetTranslation(newWord, user.LanguagePreference) != null) 
                    {
                        translation = _wordRepository.GetTranslation(newWord, user.LanguagePreference);
                    }
                    //Translate the word if it. 
                    else 
                    {
                        translation = translator.TranslateWord(newWord, user.LanguagePreference);

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
                    UserWord userWord = new UserWord() { 
                        UserId = user.Id,
                        WordId = newWord.Id,
                    };

                    _userWordRepository.CreateUserWord(userWord);
                    _userWordRepository.SaveChanges();


                    //Compose the content for the email 
                    plainTextContext = $"Hello {user.FirstName}, \n Your {user.LanguagePreference} word of the day is {translation} which in english means {newWord.Word}.";


                }
                else
                {

                    //Compose an email letting the user know that they have completed the current dictionary set.

                    plainTextContext = $"Hello {user.FirstName}, \n We are all out of words for you at the moment ";

                }

                //Components needed for the email.
                var apiKey = Environment.GetEnvironmentVariable("EMAIL_API_KEY");
                var client = new SendGridClient(apiKey);
                var from = new EmailAddress("EMAIL_SENDER");
                var to = new EmailAddress($"{user.Email}");
                var subject = "New Vocabulary Word";
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContext, "");
                var response = await client.SendEmailAsync(msg);



                //send the email.
            
            }


        }
    }
}
