﻿using DK.PolicyService.API.Converters;
using Newtonsoft.Json;

namespace DK.PolicyService.API.Commands.Dtos;

[JsonConverter(typeof(QuestionAnswerConverter))]
public abstract class QuestionAnswer
{
    public string QuestionCode { get; set; }
    public abstract QuestionType QuestionType { get; }
    public abstract object GetAnswer();
}

public enum QuestionType
{
    Text,
    Numeric,
    Choice
}

public abstract class QuestionAnswer<T> : QuestionAnswer
{
    public T Answer { get; set; }

    public override object GetAnswer()
    {
        return Answer;
    }
}

public class TextQuestionAnswer : QuestionAnswer<string>
{
    public override QuestionType QuestionType => QuestionType.Text;
}

public class NumericQuestionAnswer : QuestionAnswer<decimal>
{
    public override QuestionType QuestionType => QuestionType.Numeric;
}

public class ChoiceQuestionAnswer : QuestionAnswer<string>
{
    public override QuestionType QuestionType => QuestionType.Choice;
}