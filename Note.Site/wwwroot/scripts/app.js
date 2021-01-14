var renderMarkdown = function () {
    let textareaElement = document.getElementById('div_editor');
    let previewElement = document.getElementById('div_markdown');

    textareaElement.addEventListener("keyup", (ev) => {
        previewElement.innerHTML = marked(ev.target.value);
    });
}