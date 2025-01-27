namespace AvaTradeApp.Domain.Statics
{
    public static class MailInfo
    {
        public static readonly string Host = "smtp.gmail.com";
        public static readonly int Port = 587;
        public static readonly string Subject = "Hourly News";
        public static readonly string TemplateFirst = "<!DOCTYPE html>\r\n<html lang=\"en\">\r\n<head>\r\n    <meta charset=\"UTF-8\">\r\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\r\n    <title>News List</title>\r\n    <style>\r\n        body {\r\n            font-family: Arial, sans-serif;\r\n            margin: 0;\r\n            padding: 0;\r\n            background-color: #f9f9f9;\r\n        }\r\n        .container {\r\n            max-width: 800px;\r\n            margin: 20px auto;\r\n            padding: 20px;\r\n            background-color: #fff;\r\n            border-radius: 8px;\r\n            box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);\r\n        }\r\n        .news-item {\r\n            border-bottom: 1px solid #ddd;\r\n            padding: 15px 0;\r\n        }\r\n        .news-item:last-child {\r\n            border-bottom: none;\r\n        }\r\n        .title {\r\n            font-size: 1.2rem;\r\n            font-weight: bold;\r\n            margin-bottom: 5px;\r\n        }\r\n        .author {\r\n            color: #555;\r\n            margin-bottom: 10px;\r\n        }\r\n        .description {\r\n            margin-bottom: 10px;\r\n        }\r\n        .published {\r\n            color: #999;\r\n            font-size: 0.9rem;\r\n        }\r\n        a.article-link {\r\n            color: #007bff;\r\n            text-decoration: none;\r\n        }\r\n        a.article-link:hover {\r\n            text-decoration: underline;\r\n        }\r\n    </style>\r\n</head>\r\n<body>\r\n    <div class=\"container\">";
        public static readonly string TemplateLast = " </div>\r\n</body>\r\n</html>\r\n";
    }
}
