namespace better_browser;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private void OnGoClicked(object sender, EventArgs e)
    {
        string url = urlEntry.Text?.Trim();

        if (!string.IsNullOrEmpty(url))
        {
            if (IsValidDomain(url))
            {
                if (!url.StartsWith("http://") && !url.StartsWith("https://"))
                {
                    url = "https://" + url;
                }
                webView.Source = url;
            }
            else
            {
                string searchUrl = "https://www.google.com/search?q=" + Uri.EscapeDataString(url);
                webView.Source = searchUrl;
            }
        }
    }

    private bool IsValidDomain(string input)
    {
        return input.Contains(".") && !input.Contains(" ");
    }

    private void OnBackClicked(object sender, EventArgs e)
    {
        if (webView.CanGoBack)
            webView.GoBack();
    }

    private void OnForwardClicked(object sender, EventArgs e)
    {
        if (webView.CanGoForward)
            webView.GoForward();
    }

    private void OnRefreshClicked(object sender, EventArgs e)
    {
        webView.Reload();
    }
}
