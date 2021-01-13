var setDarkMode = function (isDarkModeEnabled) {

    if (isDarkModeEnabled) {
        document.documentElement.setAttribute('data-theme', 'dark');
    } else {
        document.documentElement.setAttribute('data-theme', 'light');
    }
}
