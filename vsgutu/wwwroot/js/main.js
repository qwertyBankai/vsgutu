$(document).ready(function(){

    $('#lection').click(function(){
        if($('#tab-lection').css('display', 'none')){
            $('#tab-lection').show('slow');

            $('#tab-practics').hide('slow');
            $('#tab-labs').hide('slow');
            $('#tab-pos').hide('slow');
            $('#tab-stat').hide('slow');

        }
    });

    $('#practics').click(function(){
        if($('#tab-practics').css('display', 'none')){
            $('#tab-practics').show('slow');

            $('#tab-lection').hide('slow');
            $('#tab-labs').hide('slow');
            $('#tab-pos').hide('slow');
            $('#tab-stat').hide('slow');
        }
    });

    $('#labs').click(function(){
        if($('#tab-labs').css('display', 'none')){
            $('#tab-labs').show('slow');

            $('#tab-practics').hide('slow');
            $('#tab-lection').hide('slow');
            $('#tab-pos').hide('slow');
            $('#tab-stat').hide('slow');
        }
    });

    $('#pos').click(function(){
        if($('#tab-pos').css('display', 'none')){
            $('#tab-pos').show('slow');

            $('#tab-labs').hide('slow');
            $('#tab-practics').hide('slow');
            $('#tab-lection').hide('slow');
            $('#tab-stat').hide('slow');
        }
    });

    $('#positionF').click(function () {
        if ($('#tab-pos').css('display', 'none')) {
            $('#tab-pos').show('slow');

            $('#tab-labs').hide('slow');
            $('#tab-practics').hide('slow');
            $('#tab-lection').hide('slow');
            $('#tab-stat').hide('slow');
        }
    });

    $('#pose').click(function(){
        if($('#tab-pos').css('display', 'none')){
            $('#tab-pos').show('slow');

            $('#tab-labs').hide('slow');
            $('#tab-practics').hide('slow');
            $('#tab-lection').hide('slow');
            $('#tab-stat').hide('slow');
        }
    });


    $('#statistics').click(function(){
        if($('#tab-stat').css('display', 'none')){
            $('#tab-stat').show('slow');

            $('#tab-pos').hide('slow');
            $('#tab-labs').hide('slow');
            $('#tab-practics').hide('slow');
            $('#tab-lection').hide('slow');
        }
    });








    $('#student').click(function(){
        if($('#create-student').css('display', 'none')){
            $('#create-student').show('slow');

            $('#create-group').hide('slow');
            $('#create-admin').hide('slow');
            $('#create-teacher').hide('slow');
        }
    });

    $('#group').click(function(){
        if($('#create-group').css('display', 'none')){
            $('#create-group').show('slow');

            $('#create-student').hide('slow');
            $('#create-admin').hide('slow');
            $('#create-teacher').hide('slow');
        }
    });

    $('#admin').click(function(){
        if($('#create-admin').css('display', 'none')){
            $('#create-admin').show('slow');

            $('#create-student').hide('slow');
            $('#create-teacher').hide('slow');
            $('#create-group').hide('slow');

        }
    });

    $('#teacher').click(function(){
        if($('#create-teacher').css('display', 'none')){
            $('#create-teacher').show('slow');

            $('#create-student').hide('slow');
            $('#create-group').hide('slow');
            $('#create-admin').hide('slow');
            
            
        }
    });

    $('#delete-group').click(function(){
        if($('#delete').css('display', 'none')){
            $('#delete').show('slow');

            $('#create').hide('slow');
        }
    });

    $('#creates-group').click(function(){
        if($('#create').css('display', 'none')){
            $('#create').show('slow');

            $('#delete').hide('slow');
        }
    });


    $('#atte-table').click(function(){
        if($('#atte').css('display', 'none')){
            $('#atte').show('slow');

            $('#pos').hide('slow');
        }
    });

    $('#pos-table').click(function(){
        if($('#pos').css('display', 'none')){
            $('#pos').show('slow');

            $('#atte').hide('slow');
        }
    });
    





    

   /* $('#lection-btn').click(function(){
        if($('#practics').css('display', 'none')){
            $('#practics').show('slow');

            $('#lection').hide('slow');
            $('#lr').hide('slow');
        }
    });

    $('#practics-btn-back').click(function(){
        if($('#lection').css('display', 'none')){
            $('#lection').show('slow');

            $('#practics').hide('slow');
            $('#lr').hide('slow');
        }
    });


    $('#practics-btn').click(function(){
        if($('#lr').css('display', 'none')){
            $('#lr').show('slow');

            $('#practics').hide('slow');
            $('#lection').hide('slow');
        }
    });

    $('#lr-btn-back').click(function(){
        if($('#practics').css('display', 'none')){
            $('#practics').show('slow');

            $('#lection').hide('slow');
            $('#lr').hide('slow');
        }
    });*/



});