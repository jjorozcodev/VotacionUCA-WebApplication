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
    var Usuario = $('#Usuario').val();
    var Clave = $('#Clave').val();
    var TipoUsuario = $('#TipoUsuario').val();
    
    debugger;

    $.post('/ajax/acceso', Usuario).done(function () {
        location.href = '/inicio/votaciones';
    }).fail();
}