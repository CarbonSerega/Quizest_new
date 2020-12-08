using System;
using System.Collections.Generic;
using System.Linq;

namespace Utility
{
    public static class Constants
    {
        /*File Constants*/
        public static IEnumerable<string> AllowedPreviewExtensions => new List<string> { "png", "jpg", "jpeg", "bmp" };
        public static IEnumerable<string> AllowedPreviewContentTypes => new List<string> { "pjpeg", "x-png" }.Concat(AllowedPreviewExtensions);
        public static int MaxPreviewSize => 104857600; //1Mb
        public static int FileNameLength => 8;
        public static string ImagePrefix => "image/";
        public static string GetContentType(string extension) => $"data:{ImagePrefix}{extension};base64,";


        /*QuizInfo Error Constants*/
        public static string QuizDoesNotExist(Guid id) => $"Quiz with id {id} doesn't exist";
        public static string QuizUnderLinkDoesNotExist(string link) => $"Quiz under link {link} doesn't exist";
        public static string QuizDataEmpty => "Quiz data cannot be empty";
        public static string InvalidImage => "The image is too large or has incorrect format";
        public static string OwnerNull => "Owner can not be null";
        public static string MongoDbCreationFailure(string entityName) => $"Can not create an entity {entityName} in MongoDB";


        /*Global Error Constants*/
        public static string InternalError => "Internal Server Error";
    }
}
