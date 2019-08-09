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
