using DK.ProductService.Domain;
using System.Collections.Generic;

namespace DK.ProductService.UnitTests.TestData;

public class TestQuestionFactory
{
    internal static ChoiceQuestion ChoiceQuestion()
    {
        return new ChoiceQuestion("TYP", 1, "Apartment / House", new List<Choice>
        {
            new("APT", "Apartment"),
            new("HOUSE", "House")
        });
    }

    internal static NumericQuestion NumericQuestion()
    {
        return new NumericQuestion("AREA", 2, "Area");
    }

    internal static DateQuestion DateQuestion()
    {
        return new DateQuestion("BIRTHDATE", 3, "Birhtdate");
    }
}
