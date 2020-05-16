window.CustomValidator = {
    RemoteValidate: function (url, formData) {
        $.ajax({
            url: url,
            type: "post",
            data: formData,
            success: function (response) {

                const columns = Object.keys(response)
                    .filter(function (column) {
                        return column !== 'IsValid'
                    });
                // console.log((columns));

                columns.forEach(function (column) {
                    CustomValidator.ValidateColumn(column, response[column])
                })

                if (response.IsValid) {
                    $('#newForm').submit();
                }
            },
            error: function (response) {
                console.log("error");
                console.log(response);
            },
        });
    },

    ValidateColumn: function (column, validateResult) {
        if (validateResult.IsValid === true) {
            this.UnsetErrorMessage(column);
        } else {
            const errorMessage = validateResult.Errors.join(',');
            this.SetErrorMessage(column, errorMessage);
        }
    },

    SetErrorMessage: function (id, errorMessage) {
        const errorId = this.GetErrorDomId(id);
        // console.log(errorId);
        // console.log(errorMessage);
        $(errorId).show().html(errorMessage);
    },

    UnsetErrorMessage: function (id) {
        const errorId = this.GetErrorDomId(id);
        $(errorId).hide();
    },

    GetErrorDomId: function (id) {
        return '#' + id + '-error';
    },
}