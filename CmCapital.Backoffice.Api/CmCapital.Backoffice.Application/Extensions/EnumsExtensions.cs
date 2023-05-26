using CmCapital.Backoffice.Domain.Enums;

namespace CmCapital.Backoffice.Application.Extensions;

public static class EnumsExtensions
{
    public static string ToCategory(this Category category)
    {
        return category switch
        {
            Category.Roupas => "Roupas",
            Category.Calçado => "Calçado",
            Category.Limpeza => "Limpeza",
            Category.Jardim => "Jardim",
            Category.Aplicativo => "Aplicativo",
            _ => "Aplicativo",
        };
    }
}