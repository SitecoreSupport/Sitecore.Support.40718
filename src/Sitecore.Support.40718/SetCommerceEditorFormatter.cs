

namespace Sitecore.Support.Commerce.Engine.Connect.DataProvider.Pipelines
{
  using Sitecore.Commerce.Engine.Connect.DataProvider;
  using Sitecore.Commerce.Engine.Connect.DataProvider.Definitions;
  using Sitecore.Data;
  using Sitecore.Data.Items;
  using Sitecore.Diagnostics;
  using Sitecore.Shell.Applications.ContentEditor;
  using Sitecore.Support.Commerce.Engine.Connect.DataProvider.Pipelines.ContentEditor;
  using System.Linq;
  using System.Collections.Generic;
  using System;
  using Sitecore.Web.UI.HtmlControls;
  using Sitecore.Commerce.Engine.Connect.DataProvider.Pipelines.ContentEditor;
  using Sitecore.Commerce.Engine.Connect.DataProvider.Extensions;

  /// <summary>
  /// Content editor pipeline that sets the <see cref="CommerceEditorFormatter"/> as the SC editor formatter.
  /// </summary>
  public class SetCommerceEditorFormatter
  {
    /// <summary>
    /// Processes the pipeline.
    /// </summary>
    /// <param name="args">The pipeline arguments.</param>
    public void Process(Sitecore.Shell.Applications.ContentEditor.Pipelines.RenderContentEditor.RenderContentEditorArgs args)
    {
      Assert.IsNotNull(args.EditorFormatter, "args.EditorFormatter");

      if ((args.Item != null) && ShouldUseCommerceFormatter(args.Item))
      {
        if (String.Equals(args.EditorFormatter.GetType().FullName, "Sitecore.Shell.Applications.ContentEditor.TranslatorFormatter"))
        {
          // using the extended  inherited from TranslatorFormatter
          args.EditorFormatter = new Sitecore.Support.Commerce.Engine.Connect.DataProvider.Pipelines.ContentEditor.CommerceEditorFormatter { Arguments = args };
        }
        else
        {
          // this is not clear if using the Reset Fields feature is supposed to work on the commerce items. Therefore, it's not implemented.
          args.EditorFormatter = new Sitecore.Commerce.Engine.Connect.DataProvider.Pipelines.ContentEditor.CommerceEditorFormatter { Arguments = args };
        }
      }
    }

    protected virtual bool ShouldUseCommerceFormatter(Item item)
    {
      Assert.IsNotNull(item, "item");

      return item.IsCatalogItem();
    }
  }
}