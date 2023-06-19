namespace SharpEval.Core.IO
{
    /// <summary>
    /// Defines interface to retrieve documentation
    /// </summary>
    public interface IDocumentationProvider
    {
        /// <summary>
        /// Retuns documentations
        /// </summary>
        /// <returns>Documentations grouped by command/function name</returns>
        public IReadOnlyDictionary<string, List<string>> GetDocumentations();
    }
}