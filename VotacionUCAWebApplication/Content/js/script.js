function Advertencia(idV) {
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
            BorrarVotacion(idV);
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

function NombreVotacionActual() {
    var urlenlace = new URL(location.href);
    var idVotacion = urlenlace.searchParams.get("v");
    // GET usando AJAX y JQuery
    $.get('/ajax/obtenernombrevotacion/' + idVotacion, function (resp) {
        $('#nombreEleccion').html('Candidatos - ' + resp);
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
                + '<a href="../Votaciones/Editar/?v=' + this.Id + '" class="btn-sm btn-primary"><i class="lni-pencil"></i></a>'
                + '<a href="#" onclick="Advertencia(' + this.Id + ')" class="btn-sm btn-danger"><i class="lni-trash"></i></a>'
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
                + '<input data-idE="' + this.Id + '" type="checkbox" autocomplete="off"><span class="glyphicon glyphicon-ok"></span>'
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
    $.get('/Ajax/ListarVotacionesDisponibles').done(function (resp) {
        $.each(resp, function () {
            $("#tablaVotacionesDisp").append(
                '<div class="col-sm-6">'
                +'<div class="card border-info mb-3">'
                +'<div class="card-body">'
                + '<h5 class="card-title">' + this.Descripcion + '</h5>'
                + '<p class="card-text">Código de Grupo:' + this.CodGrupo + '</p>'
                +'</div>'
                +'<a href="../Votaciones/Votar/?v=' + this.Id + '" class="btn btn-success btn-block float-right">Votar</a>'
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

function CandidatosVotacionDisponibles() {
    var urlenlace = new URL(location.href);
    var idVotacion = urlenlace.searchParams.get("v");
    // GET usando AJAX y JQuery
    $.get('/Ajax/ListarCandidatosVotacion/' + idVotacion).done(function (resp) {
        var contador = 1;

        $.each(resp, function () {
            if (contador > 12) {
                contador = 1;
            }

            $("#datosTablaCandidatos").append(
                '<div class="col-lg-3 col-md-4 col-xs-13">'
                +'<div onclick="Votar('+ this.Id +', '+ idVotacion +')" class="category-icon-item lis-bg' + contador + '">'
                +'<div class="icon-box">'
                +'<div class="icon">'
                +'<i class="lni-user"></i>'
                + '</div>'
                + '<h4>' + this.NombreCandidato + '</h4>'
                +'</div>'
                +'</div>'
                +'</div>'
            );

            contador = contador + 1;

        });
    }).fail();
    }

function CrearVotacion() {
    var descripcion = $('#Nombre').val();
    var codigo = $('#Codigo').val();
    var abierta = $('#Abierta').is(":checked");

    if (descripcion === '' || codigo === '') {
        Avisar("Complete todos los campos.");
    }
    else {
        var arreglo = new Array();
        $("#datosTablaE input").each(function () {
            if ($(this).is(":checked")) {
                arreglo.push($(this).attr('data-idE'));
            }
        });

        var data = { Descripcion: descripcion, CodGrupo: codigo, Abierto: abierta, Seleccion: arreglo };
        // POST usando AJAX y JQuery
        $.post('/ajax/crearvotacion', data).done(function (resp) {
            location.href ='../Inicio/Gestion';
        });
    }
}


function EditarVotacion() {
    var urlenlace = new URL(location.href);
    var idVotacion = urlenlace.searchParams.get("v");

    var descripcion = $('#Nombre').val();
    var codigo = $('#Codigo').val();
    var abierta = $('#Abierta').is(":checked");

    if (descripcion === '' || codigo === '') {
        Avisar("Complete todos los campos.");
    }
    else {
        //Agregar nueva lista de candidatos
        var arreglo = new Array();
        $("#datosTablaE input").each(function () {
            if ($(this).is(":checked")) {
                arreglo.push($(this).attr('data-idE'));
            }
        });

        var data = { Id: idVotacion, Descripcion: descripcion, CodGrupo: codigo, Abierto: abierta, Seleccion: arreglo };
        //// POST usando AJAX y JQuery
        $.post('/ajax/editarvotacion', data).done(function (resp) {
            location.href = '../../Inicio/Gestion';
        });
    }
}

function Votar(idCandidato, idVotacion) {
    var data = { IdCandidato: idCandidato, IdVotacion: idVotacion };
    // POST usando AJAX y JQuery
    $.post('/ajax/votar', data).done(function (resp) {
        VotoRealizado();
    });
}

function BorrarVotacion(idVotacion) {
    var data = { IdVotacion: idVotacion };
    // POST usando AJAX y JQuery
    $.post('/ajax/borrarvotacion', data).done(function (resp) {
        Swal.fire(
            'Eliminado!',
            'El registro se eliminó con éxito',
            'success'
        );
        location.href = '/inicio/gestion';
    });
}

function VotoRealizado() {
    Swal.fire({
        type: 'success',
        title: '¡Voto Realizado!',
        text: 'Gracias por participar en la votación.'
    }).then((result) => {
        location.href = '/inicio/votaciones';
    });
}

function CargarDatosVotacion() {
    var urlenlace = new URL(location.href);
    var idVotacion = urlenlace.searchParams.get("v");
    // GET usando AJAX y JQuery
    $.get('/ajax/obtenerdatosvotacion/' + idVotacion, function (resp) {
        $('#Nombre').val(resp.Descripcion);
        $('#Codigo').val(resp.CodGrupo);
        $('#Abierta').prop('checked', resp.Abierto);
        
        ListadoEstudiantesEdicion();
    });
}


function ListadoEstudiantesEdicion() {

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
                + '<input data-idE="' + this.Id + '" type="checkbox" autocomplete="off">'
                + '</label>'
                + '</div>'
                + '</div>'
                + '</div>'
            );

            contador = contador + 1;
        });

        CargarSeleccionCandidatos();

    }).fail();
}


function CargarSeleccionCandidatos() {
    var urlenlace = new URL(location.href);
    var idVotacion = urlenlace.searchParams.get("v");
    // GET usando AJAX y JQuery
    $.get('/Ajax/ListarCandidatosVotacion/' + idVotacion).done(function (resp) {
    
    var inpts = $("#datosTablaE :input");
        debugger;
    resp.forEach(function (item) {
        for (let elem of inpts) {
            var id = $(elem).attr('data-idE');

            if (id === item.IdEstudiante.toString()) {
                $(elem).attr('checked', 'checked');
            }
        }

        });

    }).fail();
}