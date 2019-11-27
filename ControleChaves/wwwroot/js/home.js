function updateMovimentacoes() {
    $.get("/api/movimentacao/abertos", function (data) {
        $("#emprestimos > tbody:last").children().remove();

        $.each(data, function (i, row) {
            var $row = $('<tr/>');

            $row.append($('<td/>').html(row.codigo));
            $row.append($('<td/>').html(row.retirada));
            $row.append($('<td/>').html(row.funcionarioRetirada.nome));
            $row.append($('<td/>').html(row.usuarioRetirada.nome));
            $row.append($('<td/>').attr("controleID", row.codigo).html('<button class="devolverChave btn btn-info">Devolver</button>'));

            $row.appendTo("#emprestimos > tbody:last");
        });
    });
}

$(document).ready(function () {
    updateMovimentacoes();

    $(document).on("click", ".devolverChave", function () {
        var controleID = parseInt($(this).parent().attr("controleID"));

        $('#modalNovaDevolucao .modal-dialog').load("/Movimentacao/Devolver", { "controleID": controleID }, function () {
            $.get("/api/funcionarios", function (data) {
                $('#funcionarioDevolucao').typeahead({
                    source: data,
                    display: "nome",
                    val: "id",
                    displayText: function (item) {
                        return item.nome;
                    },
                    updater: function (item) {
                        $('#hiddenEmployeeDevolucao').val(item.id);
                        return item;
                    }
                });
            });

            $("#formDevolucao").validate({
                submitHandler: function (form) {
                    chave = $("#hiddenChaveDevolucao").val();
                    employee = $("#hiddenEmployeeDevolucao").val();
                    date = $("#dataDevolucao").val();

                    if (chave == "" || employee == "" || date == "" || controleID == "") {
                        return;
                    }

                    var payload = {
                        Codigo: controleID,
                        FuncionarioID: parseInt(employee),
                        Data: date
                    }

                    $.ajax({
                        url: '/api/movimentacao',
                        contentType: 'application/json',
                        type: 'PUT',
                        data: JSON.stringify(payload),
                        dataType: 'json',
                        success: function (data) {
                            $("#msg").removeClass("alert-danger").addClass("alert-success").html("Devolução realizada com sucesso!").fadeIn("fast", function () {
                                $("#msg").delay(5000).fadeOut();
                                $('#modalNovaDevolucao').modal('hide');
                                updateMovimentacoes();
                            });
                        },
                        error: function () {
                            $('#msg').removeClass("alert-success").addClass("alert-danger").html('Erro ao processar a devolução').fadeIn("fast", function () {
                                $("#msg").delay(5000).fadeOut();
                            });;
                        }
                    });
                }
            });

            $('#modalNovaDevolucao').modal('show');
        });

        

        
    });

    $("#novoEmprestimo").on("click", function () {
        $('#modalNovoEmprestimo .modal-dialog').load("/Movimentacao/Create", function () {
            $.get("/api/funcionarios", function (data) {
                $('#funcionarioRetirada').typeahead({
                    source: data,
                    display: "nome",
                    val: "id",
                    displayText: function (item) {
                        return item.nome;
                    },
                    updater: function (item) {
                        $('#hiddenEmployee').val(item.id);
                        return item;
                    }
                });
            });


            $.get("/api/chaves/disponiveis", function (data) {
                $('#chave').typeahead({
                    source: data,
                    display: "nome",
                    val: "id",
                    displayText: function (item) {
                        return item.nome;
                    },
                    updater: function (item) {
                        $('#hiddenChave').val(item.id);
                        return item;
                    }
                });
            });

            $("#formEmprestimo").validate({
                submitHandler: function (form) {
                    chave = $("#hiddenChave").val();
                    employee = $("#hiddenEmployee").val();
                    date = $("#data").val();

                    if (chave == "" || employee == "" || date == "") {
                        return;
                    }

                    var payload = {
                        Data: date,
                        FuncionarioID: parseInt(employee),
                        ChaveID: chave
                    }

                    $.ajax({
                        url: '/api/movimentacao',
                        contentType: 'application/json',
                        type: 'POST',
                        data: JSON.stringify(payload),
                        dataType: 'json',
                        success: function (data) {
                            $("#msg").removeClass("alert-danger").addClass("alert-success").html("Empréstimo realizado com sucesso!").fadeIn("fast", function () {
                                $("#msg").delay(5000).fadeOut();
                                $('#modalNovoEmprestimo').modal('hide');
                                updateMovimentacoes();
                            });
                        },
                        error: function () {
                            $('#msg').removeClass("alert-success").addClass("alert-danger").html('Erro ao processar o empréstimo').fadeIn("fast", function () {
                                $("#msg").delay(5000).fadeOut();
                            });;
                        }
                    });
                }
            });

            $('#modalNovoEmprestimo').modal('show');
        });
    });
});