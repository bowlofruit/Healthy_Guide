using HealthGuide.Models;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;

namespace HealthGuide
{
    public class Validator
    {
        public static bool IsValidTitle(string title)
        {
            return !string.IsNullOrEmpty(title.Trim());
        }

        public static bool IsValidContent(string content)
        {
            return !string.IsNullOrEmpty(content.Trim());
        }

        public static bool IsValidAuthor(string author, List<Blog> blogs, ref int authorId)
        {
            if (string.IsNullOrEmpty(author.Trim())) return false;

            foreach (var blog in blogs)
            {
                if (blog.Author == author)
                {
                    authorId = blog.UserId;
                    return true;
                }
            }
            return false;
        }

        public static bool IsValidAuthor(string author, List<Review> blogs, ref int authorId)
        {
            if (string.IsNullOrEmpty(author.Trim())) return false;

            foreach (var blog in blogs)
            {
                if (blog.Author == author)
                {
                    authorId = blog.UserId;
                    return true;
                }
            }
            return false;
        }

        public static bool IsValidEmail(string email, List<User> users)
        {
            if (string.IsNullOrEmpty(email.Trim())) return false;

            try
            {
                bool unique = true;
                foreach (var user in users)
                {
                    if (user.Email == email)
                    {
                        unique = false;
                        break;
                    }
                }

                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email && unique;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsValidPhoneNumber(string number, List<User> users)
        {
            if (string.IsNullOrEmpty(number.Trim())) return false;

            bool unique = true;
            foreach (var user in users)
            {
                if (user.PhoneNumber == number)
                {
                    unique = false;
                    break;
                }
            }

            if (number[0] == '+')
            {
                return Regex.Match(number, @"^\+380\d{9}$").Success && unique;
            }
            else if (number[0] == '3')
            {
                return Regex.Match(number, @"^380\d{9}$").Success && unique;
            }
            else if (number[0] == '0')
            {
                return Regex.Match(number, @"^0\d{9}$").Success && unique;
            }
            else
            {
                return false;
            }
        }

        public static bool IsValidLogin(string login, List<User> users)
        {
            if (string.IsNullOrEmpty(login.Trim())) return false;

            bool unique = true;
            foreach (var user in users)
            {
                if (user.Login == login)
                {
                    unique = false;
                    break;
                }
            }
            return unique && login.Length > 5;
        }

        public static bool IsValidScore(string score)
        {
            if (string.IsNullOrEmpty(score.Trim()) || score.Length > 4) return false;
            bool valid =  double.TryParse(score, out double result);
            return result > 0 && valid;
        }

        public static bool IsValidDate(string date)
        {
            if (string.IsNullOrEmpty(date)) return false;
            return DateTime.TryParse(date, out DateTime result);
        }

        public static bool IsValidKcal(string kcal)
        {
            if (string.IsNullOrEmpty(kcal)) return false;
            return uint.TryParse(kcal, out uint result);
        }

        public static bool IsValidServing(string servings)
        {
            if (string.IsNullOrEmpty(servings)) return false;
            return uint.TryParse(servings, out uint result);
        }

        public static bool IsValidTime(string cookingTime)
        {
            if (string.IsNullOrEmpty(cookingTime)) return false;
            return uint.TryParse(cookingTime, out uint result);
        }
    }
}