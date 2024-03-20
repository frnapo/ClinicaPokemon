function Find(NrMicro) {
    $.ajax({
        method: 'POST',
        url: '/Animali/FindYourPokemonChip',
        data: { chip: NrMicro },
        success: function (risultato) {
            if (risultato === 0) {
                $('#IsNotFound').removeClass('d-none');
            } else {
                // Redirect alla pagina di dettaglio dell'animale
                window.location.href = '/Animali/DettagliPokemon/' + risultato.idAnimale;
            }
        },
        error: function (e) {
            console.log(e);
        }
    });
}

$('#Find').click(function () {
    Find($('#NrMicro').val());
});