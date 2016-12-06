$(document).ready(function () {
    $('#productForm')
        .formValidation({
            framework: 'bootstrap',
            icon: {
                valid: 'glyphicon glyphicon-ok',
                invalid: 'glyphicon glyphicon-remove',
                validating: 'glyphicon glyphicon-refresh'
            },
            excluded: ':disabled',
            fields: {
                name: {
                    validators: {
                        notEmpty: {
                            message: 'The name is required'
                        },
                        stringLength: {
                            min: 6,
                            max: 30,
                            message: 'The name must be more than 6 and less than 30 characters long'
                        }
                    }
                },
                description: {
                    validators: {
                        notEmpty: {
                            message: 'The description is required'
                        },
                        stringLength: {
                            min: 50,
                            max: 1000,
                            message: 'The description must be more than 50 and less than 1000 characters'
                        }
                    }
                },
                price: {
                    validators: {
                        notEmpty: {
                            message: 'The price is required'
                        },
                        numeric: {
                            message: 'The price must be a number'
                        }
                    }
                },
                size: {
                    validators: {
                        notEmpty: {
                            message: 'The size is required'
                        }
                    }
                },
                color: {
                    validators: {
                        notEmpty: {
                            message: 'The color is required'
                        }
                    }
                }
            }
        })
        /* Using Combobox for color and size select elements */
        .find('[name="color"], [name="size"]')
            .combobox()
            .end()
});