function Advertencia() {
    Swal.fire({
        title: '¿Estás seguro que deseas eliminar este registro?',
        type: 'warning',
        showCancelButton: true,
        cancelButtonText: 'Cancelar',
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Sí, eliminar!'
    }).then((result) => {
        if (result.value) {
            Swal.fire(
                'Eliminado!',
                'El registro se eliminó con éxito',
                'success'
            );
        }
    });
}

function Avisar(texto){
    Swal.fire(texto);
}

function Acceso() {
    var usuario = $('#Usuario').val();
    var clave = $('#Clave').val();
    var tipousuario = $('#TipoUsuario').is(":checked");

    if (usuario === '' || clave === '') {
        Avisar("Complete todos los campos.");
    }
    else {
        var data = { Usuario: usuario, Clave: clave, TipoUsuario: tipousuario };
        // POST usando AJAX y JQuery
        $.post('/ajax/acceso', data).done(function (resp) {

            if (resp !== '') {
                if (resp.Gestiona) {
                    location.href = '/inicio/gestion';
                }
                else
                    location.href = '/inicio/votaciones';
            }
            else {
                Avisar("¡Usuario o Contraseña incorrectos!");
            }
        });
    }
}

function UsuarioActual() {

    // GET usando AJAX y JQuery
    $.get('/ajax/obtenerusuarioactual', function (resp) {
        $('#usuarioActual').html('' + resp);
    });
}

function ListarVotaciones() {

    // GET usando AJAX y JQuery
    $.get('/Ajax/ListarVotaciones').done(function (resp) {

        $.each(resp, function () {

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
                + '<a href= "../Votaciones/Candidatos/?v=' + this.Id + '" class="btn-sm btn-warning"><i class="lni-eye" style="color:white"></i></a>'
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
    $.get('/Ajax/ListarEstudiantes').done(function (resp) {
        var contador = 1;

        $.each(resp, function () {
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
    $.get('/Ajax/ListarVotaciones').done(function (resp) {
        $.each(resp, function () {

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

function ListarCandidatosVotacion() {
    var urlenlace = new URL(location.href);
    var idVotacion = urlenlace.searchParams.get("v");

    // GET usando AJAX y JQuery
    $.get('/Ajax/ListarCandidatosVotacion/' + idVotacion).done(function (resp) {
        $.each(resp, function () {

            $("#tablaCandidatosVotacion").append(
                '<tr>'
                +'<td>'+ this.Id +'</td>'
                +'<td>'+ this.NombreCandidato +'</td>'
                +'<td>'+ this.VotosObtenidos +'</td>'
                +'</tr>'
            );

        });
    }).fail();
}
