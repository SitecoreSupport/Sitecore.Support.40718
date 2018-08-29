using Sitecore.Support.Commerce.Engine.Connect.DataProvider.Pipelines.ContentEditor;
using Sitecore.Shell.Applications.ContentEditor;

namespace Sitecore.Support.Commerce.Engine.Connect.DataProvider.Pipelines
{
  //using Sitecore.Commerce.Engine.Connect.DataProvider.Pipelines.ContentEditor;
  using Sitecore.Diagnostics;

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
      // As there are mutliple types of editors in Sitecore, we need to override each one and make sure we create an appropriate
      // override compared to the type that is already set.
      Assert.IsNotNull(args.EditorFormatter, "args.EditorFormatter");

      if (args.EditorFormatter.GetType().FullName == "Sitecore.Shell.Applications.ContentEditor.TranslatorFormatter")
      { // ALEX: when clicking "Translate" button
        args.EditorFormatter = new CommerceEditorFormatter { Arguments = args };
      }
      else
      { // ALEX: when un-clicking "Translate" button
        args.EditorFormatter = new EditorFormatter { Arguments = args };
      }
    }
  }
}