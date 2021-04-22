using DalServerDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SerialDeserial = TestLib.SerialDeserial;
using XmlTest = TestLib.Test;
using XmlQuestion = TestLib.Question;
using XmlAnswer = TestLib.Answer;

namespace Server
{
    class TestConverter
    {
        

        public static Test convert(string file)
        {
            SerialDeserial serialDeserial = new SerialDeserial();
            XmlTest xmlTest = serialDeserial.Deserialize<XmlTest>(file);
            Test test = new Test();
            test.Author = xmlTest.Author;
            test.Title = xmlTest.Title;
            test.DtCreate = DateTime.Now;
            test.Questions = xmlTest.Questions.Select(xq => convert(xq)).ToList();
            return test;
        }

        public static Question convert(XmlQuestion xmlQuestion)
        {
            Question question = new Question();
            question.Title = xmlQuestion.Description;
            question.Difficalty =  Convert.ToInt32( xmlQuestion.Difficulty);
            question.Answers = xmlQuestion.Answer.Select(a => convert(a)).ToList();
            return question;
        }

        public static Answer convert(XmlAnswer xmlAnswer)
        {
            Answer answer = new Answer();
            answer.Title = xmlAnswer.Description;
            answer.isRight = Convert.ToBoolean(xmlAnswer.IsRight);
            return answer;
        }
    }
}

