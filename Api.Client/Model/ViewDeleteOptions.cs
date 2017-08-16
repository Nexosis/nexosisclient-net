namespace Nexosis.Api.Client.Model
{
    public class ViewDeleteOptions
    {

        /// <summary>
        /// Options for cascading the delete.<br/>
        /// When cascade=sessions, also deletes sessions associated with the view
        /// </summary>
        public ViewCascadeOptions Cascade { get; set; }
    }
}