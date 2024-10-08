﻿using DK.ProductService.API.Commands.Results;
using DK.ProductService.API.Commands;
using DK.ProductService.Domain.Contracts;
using DK.ProductService.Domain;
using MediatR;
using DK.ProductService.API.Commands.Dtos;

namespace DK.ProductService.Commands;

public class CreateProductDraftHandler : IRequestHandler<CreateProductDraftCommand, CreateProductDraftResult>
{
    private readonly IProductRepository products;

    public CreateProductDraftHandler(IProductRepository products)
    {
        this.products = products;
    }

    public async Task<CreateProductDraftResult> Handle(CreateProductDraftCommand request,
        CancellationToken cancellationToken)
    {
        var draft = Product.CreateDraft
        (
            request.ProductDraft.Code,
            request.ProductDraft.Name,
            request.ProductDraft.Image,
            request.ProductDraft.Description,
            request.ProductDraft.MaxNumberOfInsured,
            request.ProductDraft.Icon
        );

        foreach (var cover in request.ProductDraft.Covers)
            draft.AddCover(cover.Code, cover.Name, cover.Description, cover.Optional, cover.SumInsured);

        var questions = new List<Question>();
        foreach (var question in request.ProductDraft.Questions)
            switch (question)
            {
                case NumericQuestionDto numericQuestion:
                    questions.Add(new NumericQuestion(numericQuestion.QuestionCode, numericQuestion.Index,
                        numericQuestion.Text));
                    break;
                case DateQuestionDto dateQuestion:
                    questions.Add(new DateQuestion(dateQuestion.QuestionCode, dateQuestion.Index,
                        dateQuestion.Text));
                    break;
                case ChoiceQuestionDto choiceQuestion:
                    questions.Add(new ChoiceQuestion(choiceQuestion.QuestionCode, choiceQuestion.Index,
                        choiceQuestion.Text, choiceQuestion.Choices.Select(c => new Choice(c.Code, c.Label)).ToList()));
                    break;
            }

        draft.AddQuestions(questions);

        await products.Add(draft);

        return new CreateProductDraftResult
        {
            ProductId = draft.Id
        };
    }
}
