namespace PeekLinkBot.Scrapper
{
    /// <summary>
    ///     Interface that represents a class capable of scrapping a certain document and extract a string containing
    ///     some information from the document
    /// </summary>
    public interface IScrapper
    {
        /// <summary>
        ///     Gets a string with informations about the document
        /// </summary>
        string GetPageInfo();
    }
}