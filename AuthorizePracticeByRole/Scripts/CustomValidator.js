window.CustomValidator = {
    
    // response 格式要參考 ValidateResultGroup
    // property 原則上依照後端給定的
    
    RemoteValidate: function (url, formData) {
        $.ajax({
            url: url,
            type: "post",
            data: formData,
            success: function (response) {

                const columns = Object.keys(response)
                    .filter(function (column) {
                        return column !== CustomValidator.IsValidColumnName
                    });
                // console.log((columns));

                columns.forEach(function (column) {
                    CustomValidator.ValidateColumn(column, response[column])
                })

                if (response[CustomValidator.IsValidColumnName]) {
                    $('#newForm').submit();
                }
            },
            error: function (response) {
                console.log("error");
                console.log(response);
            },
        });
    },

    IsValidColumnName : 'IsValid',

    ValidateColumn: function (column, validateResult) {
        if (validateResult[CustomValidator.IsValidColumnName] === true) {
            CustomValidator.UnsetErrorMessage(column);
        } else {
            const errorMessage = validateResult.Errors.join(',');
            CustomValidator.SetErrorMessage(column, errorMessage);
        }
    },

    SetErrorMessage: function (id, errorMessage) {
        const errorId = CustomValidator.GetErrorDomId(id);
        // console.log(errorId);
        // console.log(errorMessage);
        $(errorId).show().html(errorMessage);
    },

    UnsetErrorMessage: function (id) {
        const errorId = CustomValidator.GetErrorDomId(id);
        $(errorId).hide();
    },

    GetErrorDomId: function (id) {
        return '#' + id + '-error';
    },
}