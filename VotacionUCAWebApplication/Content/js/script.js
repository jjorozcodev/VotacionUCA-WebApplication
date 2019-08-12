function Advertencia() {
    Swal.fire({
        title: '¿Estás seguro que deseas eliminar este registro?',
        type: 'warning',
        showCancelButton: true,
        cancelButtonText: 'Cancelar',
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Si, eliminar!'
    }).then((result) => {
        if (result.value) {
            Swal.fire(
                'Eliminado!',
                'El registro se eliminó con éxito',
                'success'
            )
        }
    })
}

function Acceso() {
    var usuario = $('#Usuario').val();
    var clave = $('#Clave').val();
    var tipousuario = $('#TipoUsuario').is(":checked");

    var data = { Usuario: usuario, TipoUsuario: tipousuario };
    // POST usando AJAX y JQuery
    $.post('/ajax/acceso', data).done(function (resp) {
        if (resp) {
            if (tipousuario) {
                location.href = '/inicio/votaciones';
            }
            else
                location.href = '/inicio/gestion';
        }
    });
}

function UsuarioActual() {

    // GET usando AJAX y JQuery
    $.get('/ajax/obtenerusuarioactual', function (data) {
        $('#usuarioActual').html('' + data);
    });
}

function ListarVotaciones() {

    // GET usando AJAX y JQuery
    $.get('/Ajax/ListarVotaciones').done(function (data) {
        debugger;
        $.each(data, function () {

            var estado = "";
            if (this.Abierto === true) {
                estado = "Abierto";
            }
            else {
                estado = "Cerrado";
            }

            $("#datosTabla").append(
                '<tr>'
                + '<td>' + this.Id + '</td>'
                + '<td>' + this.Descripcion + '</td>'
                + '<td>' + this.CodGrupo + '</td>'
                + '<td>' + estado + '</td>'
                + '<td>'
                + '<a href= "../Votaciones/Candidatos/' + this.Id + '" class="btn-sm btn-warning"><i class="lni-eye" style="color:white"></i></a>'
                + '<a href="../Votaciones/Editar/' + this.Id + '" class="btn-sm btn-primary"><i class="lni-pencil"></i></a>'
                + '<a href="#" onclick="Advertencia()" class="btn-sm btn-danger"><i class="lni-trash"></i></a>'
                + '</td>'
                + '<tr>'
                
            );
        });
    }).fail();
}

function ListarEstudiantes() {

    // GET usando AJAX y JQuery
    $.get('/Ajax/ListarEstudiantes').done(function (data) {
        var contador = 1;

        $.each(data, function () {
            if (contador > 12) {
                contador = 1;
            }

            $("#datosTablaE").append(
                '<div class="col-lg-3 col-md-4 col-xs-13">'
                + '<div class="category-icon-item lis-bg' + contador + '">'
                + '<div class="icon-box">'
                + '<i class="lni-user"></i>'
                + '<h4>' + this.NombreCompleto + '</h4>'
                + '<p class="categories-listing">Carnet:' + this.NumCarnet + '</p>'
                + '<label class="btn btn-default">'
                + '<input type="checkbox" autocomplete="off"><span class="glyphicon glyphicon-ok"></span>'
                + '</label>'
                + '</div>'
                + '</div>'
                + '</div>'
            );

            contador = contador + 1;
        });
    }).fail();
}

function ListarVotacionesDisponibles() {

    // GET usando AJAX y JQuery
    $.get('/Ajax/ListarVotaciones').done(function (data) {
        $.each(data, function () {

            $("#tablaVotacionesDisp").append(
                '<div class="col-sm-6">'
                +'<div class="card border-info mb-3">'
                +'<div class="card-body">'
                + '<h5 class="card-title">' + this.Descripcion + '</h5>'
                + '<p class="card-text">Código de Grupo:' + this.CodGrupo + '</p>'
                +'</div>'
                +'<a href="../Votaciones/Votar" class="btn btn-success btn-block float-right">Votar</a>'
                +'</div>'
                +'</div>'
            );
        });
    }).fail();
}
