/*
 * Because my API keys are from free services, it's not so confidential
 * But in case of using sensitive information, you will want to add this file to .gitignore
 * Then when building on a new environment, this will will have to be reproduced
 * An example.Env.cs file can be used to signal what variables need to be set up for production
*/

namespace APICollection
{
    public class Env
    {
        public static string _weatherSecret = "3537e3cf3eb69eb1b7bc8121d4e9710f";
        public static string _cryptoSecret = "29A329F3-3FFF-4E4F-B1AC-BDBD66E90A5A";
    }
}
