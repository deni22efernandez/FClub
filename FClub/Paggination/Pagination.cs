using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace FClub.Paggination
{
	[HtmlTargetElement("div", Attributes ="Pagin")]
	public class Pagination : TagHelper
	{
		public PageInfo Pagin { get; set; }
		public bool ClassEnabled { get; set; }
		public string PageClass { get; set; }
		public string PageClassNormal { get; set; }
		public string PageClassSelected { get; set; }
		public override void Process(TagHelperContext context, TagHelperOutput output)
		{
			var result = new TagBuilder("div");
			for (int i = 1; i < Pagin.TotalPages; i++)
			{
				TagBuilder builder = new TagBuilder("a");
				string url = Pagin.Uri.Replace(":", i.ToString());
				builder.Attributes["href"] = url;
				if (ClassEnabled)
				{
					builder.AddCssClass(PageClass);
					if (Pagin.CurrentPage == i)
					{
						builder.AddCssClass(PageClassSelected);
					}
					else
					{
						builder.AddCssClass(PageClassNormal);
					}
				}
				builder.InnerHtml.Append(i.ToString());
				result.InnerHtml.AppendHtml(builder);
			}
			output.Content.AppendHtml(result.InnerHtml);
			
		}
	}
}
