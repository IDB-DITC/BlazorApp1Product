using Microsoft.AspNetCore.Components;

namespace BlazorApp1Product.Shared
{
	public partial class Grid<TItem>
	{
		[Parameter]
		public RenderFragment Header { get; set; } = default!;
		[Parameter]
		public RenderFragment<TItem> Row { get; set; } = default!;
		[Parameter]
		public RenderFragment Footer { get; set; } = default!;
		[Parameter]
		public List<TItem> Items { get; set; } = default!;
	}
}
