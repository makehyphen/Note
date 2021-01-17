
/// Dark mode

var setDarkMode = function (isDarkModeEnabled) {

    if (isDarkModeEnabled) {
        document.documentElement.setAttribute('data-theme', 'dark');
    } else {
        document.documentElement.setAttribute('data-theme', 'light');
    }
}

/// Render markdown
let alreadyAddedEvent = false;
var renderMarkdown = function (doItAgain) {
    if (!alreadyAddedEvent) {
        let textareaElement = document.getElementById('div_editor');
        let previewElement = document.getElementById('div_markdown');

        if (doItAgain) {
            textareaElement.addEventListener("keyup", (ev) => {
                previewElement.innerHTML = marked(ev.target.value);
                doItAgain = false;
                alreadyAddedEvent = true;
            });
        }
    }
}

var renderMarkdownNow = function (value) {
    let previewElement = document.getElementById('div_markdown');
    if (previewElement != null) {
        previewElement.innerHTML = marked(value);
    }
}

/// Input width

var setWidthInput = function () {
    var bookElement = document.getElementById("inputBook");
    var pageElement = document.getElementById("inputPage");

    if (bookElement != undefined) {
        bookElement.style.width = ((bookElement.value.length + 0) * 8) + 'px';
    }

    if (pageElement != undefined) {
        pageElement.style.width = ((pageElement.value.length + 0) * 8) + 'px';
    }
}

/// Scroll align

var Enabled = true;
var Width = 'Width';
var Height = 'Height';
var Top = 'Top';
var Left = 'Left';
var scroll = 'scroll';
var client = 'client';
var EventListener = 'EventListener';
var addEventListener = 'add' + EventListener;
var length = 'length';
var Math_round = Math.round;

var names = {};

var setState = function (enabled) {
    Enabled = enabled;
}

var reset = function () {
    var elems = document.getElementsByClassName('sync' + scroll);

    // clearing existing listeners
    var i, j, el, found, name;
    for (name in names) {
        if (names.hasOwnProperty(name)) {
            for (i = 0; i < names[name][length]; i++) {
                names[name][i]['remove' + EventListener](
                    scroll, names[name][i].syn, 0
                );
            }
        }
    }

    // setting-up the new listeners
    for (i = 0; i < elems[length];) {
        found = j = 0;
        el = elems[i++];
        if (!(name = el.getAttribute('name'))) {
            // name attribute is not set
            continue;
        }

        el = el[scroll + 'er'] || el;  // needed for intence

        // searching for existing entry in array of names;
        // searching for the element in that entry
        for (; j < (names[name] = names[name] || [])[length];) {
            found |= names[name][j++] == el;
        }

        if (!found) {
            names[name].push(el);
        }

        el.eX = el.eY = 0;

        el.addEventListener(scroll, (e) => {
            if (Enabled) {
                var elems = names[name];

                el = e.srcElement;

                var scrollX = el[scroll + Left];
                var scrollY = el[scroll + Top];

                var xRate =
                    scrollX /
                    (el[scroll + Width] - el[client + Width]);
                var yRate =
                    scrollY /
                    (el[scroll + Height] - el[client + Height]);

                var updateX = scrollX != el.eX;
                var updateY = scrollY != el.eY;

                var otherEl, i = 0;

                el.eX = scrollX;
                el.eY = scrollY;

                for (; i < elems[length];) {
                    otherEl = elems[i++];
                    if (otherEl != el) {
                        if (updateX && Math_round(otherEl[scroll + Left] - (scrollX = otherEl.eX = Math_round(xRate * (otherEl[scroll + Width] - otherEl[client + Width])))
                        )
                        ) {
                            otherEl[scroll + Left] = scrollX;
                        }

                        if (updateY &&
                            Math_round(
                                otherEl[scroll + Top] -
                                (scrollY = otherEl.eY =
                                    Math_round(yRate *
                                        (otherEl[scroll + Height] -
                                            otherEl[client + Height]))
                                )
                            )
                        ) {
                            otherEl[scroll + Top] = scrollY;

                            var selector = "#app > div > div.app-panel-right.col-right.flex-1.float-left > div.container.clearfix.app-text-container.pb-3 > div.col-6.float-left.pl-textarea.app-textarea-container.overflow-hidden.pr-1"
                            var element = document.querySelector(selector);

                            if (element.className.indexOf("pt-4") < -1) {
                                element.classList.add("pt-4");
                            }
                        }
                    }
                }
            }
        }, 0);
    }
}
