//Show Errors
function error_handler(e) {
    if (e.errors) {
        var message = "<strong>Ошибки:</strong>";
        message += "<ul>"
        $.each(e.errors, function (key, value) {

            if ('errors' in value) {
                $.each(value.errors, function () {
                    message += "<li>" + this + "<br></li>";
                });
            }
        });
        message += "</ul>"
        showError(message);
        this.cancelChanges();
    }
}

function setSGridHeight(grid,height) {
    
    grid.element.height(height);
    grid.resize(true);
}