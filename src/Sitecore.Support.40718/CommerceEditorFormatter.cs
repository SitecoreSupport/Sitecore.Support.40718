using Sitecore.Commerce.Engine.Connect.DataProvider.Pipelines.ContentEditor;
namespace Sitecore.Support.Commerce.Engine.Connect.DataProvider.Pipelines.ContentEditor
{
  using System.Collections.Generic;
  using System.Web.UI;
  using Sitecore.Commerce.Engine.Connect.DataProvider.Definitions;
  using Sitecore.Commerce.Engine.Connect.DataProvider;
  using Sitecore.Data;
  using Sitecore.Diagnostics;
  using Sitecore.Shell.Applications.ContentEditor;

  /// <summary>
  /// Customized EditorFormatter for Commerce content items.
  /// </summary>
  public class CommerceEditorFormatter : TranslatorFormatter // ALEX: Changed to use TranslatorFormatter
  {
    /// <summary>
    /// Renders a field.
    /// </summary>
    /// <param name="parent">The parent control.</param>
    /// <param name="field">The editor field.</param>
    /// <param name="readOnly">Specifies whether the field should be rendered as read-only.</param>
    public override void RenderField(
        Control parent,
        Shell.Applications.ContentManager.Editor.Field field,
        bool readOnly)
    {
      base.RenderField(
          parent,
          field,
          readOnly || ShouldMakeReadOnly(field));
    }

    /// <summary>
    /// Renders a field.
    /// </summary>
    /// <param name="parent">The parent control.</param>
    /// <param name="field">The editor field.</param>
    /// <param name="fieldType">The field type.</param>
    /// <param name="readOnly">Specifies whether the field should be rendered as read-only.</param>
    public new void RenderField(
        Control parent,
        Sitecore.Shell.Applications.ContentManager.Editor.Field field,
        Sitecore.Data.Items.Item fieldType,
        bool readOnly)
    {
      base.RenderField(
          parent,
          field,
          fieldType,
          readOnly || ShouldMakeReadOnly(field));
    }

    /// <summary>
    /// Renders a field.
    /// </summary>
    /// <param name="parent">The parent control.</param>
    /// <param name="field">The editor field.</param>
    /// <param name="fieldType">The field type.</param>
    /// <param name="readOnly">Specifies whether the field should be rendered as read-only.</param>
    /// <param name="value">The value.</param>
    public override void RenderField(
        Control parent,
        Sitecore.Shell.Applications.ContentManager.Editor.Field field,
        Sitecore.Data.Items.Item fieldType,
        bool readOnly,
        string value)
    {
      base.RenderField(
          parent,
          field,
          fieldType,
          readOnly || ShouldMakeReadOnly(field),
          value);
    }

    /// <summary>
    /// Determines if a field should be rendered as read-only.
    /// </summary>
    /// <param name="field">The editor field.</param>
    /// <returns>True if the field should be rendered as read-only, otherwise false.</returns>
    protected virtual bool ShouldMakeReadOnly(Shell.Applications.ContentManager.Editor.Field field)
    {
      Assert.ArgumentNotNull(field, "field");
      Assert.IsNotNull(Context.ContentDatabase, "Sitecore.Context.ContentDatabase");

      var catalogTemplates =
          new List<ID>
          {
                    KnownItemIds.CatalogGeneratedFolder,
                    KnownItemIds.CatalogTemplateId,
                    KnownItemIds.CategoryTemplateId,
                    KnownItemIds.SellableItemTemplateId,
                    KnownItemIds.SellableItemVariantTemplateId
          };

      var repository = new CatalogRepository();

      return
          (catalogTemplates.Contains(field.ItemField.Item.TemplateID) || repository.TemplateExistsInMappings(field.ItemField.Item.TemplateID)) &&
          field.ItemField.InnerItem.Appearance.ReadOnly;
    }
  }
}