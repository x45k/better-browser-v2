namespace better_browser;

public partial class MainPage : ContentPage
{
    private List<WebView> tabs = new();
    private List<Button> tabButtons = new();
    private int currentTabIndex = 0;
    private bool tabsEnabled = true;

    public MainPage()
    {
        InitializeComponent();
        AddNewTab("https://new-tab.x45k.dev");
    }

    private void AddNewTab(string url)
    {
        if (!tabsEnabled)
        {
            webView.Source = url;
            return;
        }

        var newWebView = new WebView { Source = url };
        tabs.Add(newWebView);

        var tabButton = new Button
        {
            Text = $"Tab {tabs.Count} ✕",
            BackgroundColor = Colors.White,
            BorderWidth = 1,
            CornerRadius = 10,
            Padding = new Thickness(10, 5),
            FontSize = 12,
        };
        tabButton.Clicked += (sender, args) => SwitchToTab(tabButtons.IndexOf(tabButton));

        var closeButton = new Button
        {
            Text = "✕",
            FontSize = 10,
            WidthRequest = 20,
            HeightRequest = 20,
            BackgroundColor = Colors.Transparent,
            Padding = 0,
        };
        closeButton.Clicked += (sender, args) => RemoveTab(tabButton);

        var tabLayout = new HorizontalStackLayout { Spacing = 3 };
        tabLayout.Children.Add(tabButton);
        tabLayout.Children.Add(closeButton);

        tabContainer.Children.Add(tabLayout);
        tabButtons.Add(tabButton);

        SwitchToTab(tabs.Count - 1);
    }

    private void RemoveTab(Button tabButton)
    {
        int index = tabButtons.IndexOf(tabButton);
        if (index == -1 || tabs.Count == 1) return;

        tabButtons.RemoveAt(index);
        tabs.RemoveAt(index);
        tabContainer.Children.RemoveAt(index);

        if (currentTabIndex >= tabs.Count)
            currentTabIndex = tabs.Count - 1;

        SwitchToTab(currentTabIndex);
    }

    private void SwitchToTab(int index)
    {
        if (!tabsEnabled || index < 0 || index >= tabs.Count) return;

        currentTabIndex = index;
        webView.Source = tabs[index].Source;

        for (int i = 0; i < tabButtons.Count; i++)
            tabButtons[i].BackgroundColor = (i == currentTabIndex) ? Colors.LightGray : Colors.White;
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
                if (tabsEnabled)
                {
                    tabs[currentTabIndex].Source = url;
                }
                webView.Source = url;
            }
            else
            {
                string searchUrl = "https://www.google.com/search?q=" + Uri.EscapeDataString(url);
                if (tabsEnabled)
                {
                    tabs[currentTabIndex].Source = searchUrl;
                }
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
        if (webView.CanGoBack) webView.GoBack();
    }

    private void OnForwardClicked(object sender, EventArgs e)
    {
        if (webView.CanGoForward) webView.GoForward();
    }

    private void OnRefreshClicked(object sender, EventArgs e)
    {
        webView.Reload();
    }

    private void OnNewTabClicked(object sender, EventArgs e)
    {
        if (tabsEnabled)
        {
            AddNewTab("https://new-tab.x45k.dev");
        }
    }

    private void OnTabToggleToggled(object sender, ToggledEventArgs e)
    {
        tabsEnabled = e.Value;

        if (tabsEnabled)
        {
            tabBar.IsVisible = true;
            if (tabs.Count == 0) AddNewTab("https://new-tab.x45k.dev");
            SwitchToTab(0);
        }
        else
        {
            tabBar.IsVisible = false;
            webView.Source = tabs.Count > 0 ? tabs[currentTabIndex].Source : "https://new-tab.x45k.dev";
            tabs.Clear();
            tabButtons.Clear();
            tabContainer.Children.Clear();
        }
    }
}
