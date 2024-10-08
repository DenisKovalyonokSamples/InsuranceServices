using DK.ProductService.API.Queries.Converters;
using DK.ProductService.API.Queries.Types;
using Newtonsoft.Json;

namespace DK.ProductService.API.Queries.Dtos;

[JsonConverter(typeof(QuestionDtoConverter))]
public abstract class QuestionDto
{
    public string QuestionCode { get; set; }
    public int Index { get; set; }
    public string Text { get; set; }
    public abstract QuestionType QuestionType { get; }
}

public class ChoiceQuestionDto : QuestionDto
{
    public IList<ChoiceDto> Choices { get; set; }

    public override QuestionType QuestionType => QuestionType.Choice;
}

public class DateQuestionDto : QuestionDto
{
    public override QuestionType QuestionType => QuestionType.Date;
}

public class NumericQuestionDto : QuestionDto
{
    public override QuestionType QuestionType => QuestionType.Numeric;
}
