function closeJumb(){
	document.getElementById('jumbo').style.display = "none";
	document.getElementById('closeJumb').style.display = "none";
}
/*$(function(){
    // инициализации подсказок для всех элементов на странице, имеющих атрибут data-toggle="tooltip"
    $('[data-toggle="tooltip"]').tooltip();
    placement: 'top',

})*/
$( function () {
    $( "#phone" ).mask( "8(999) 999-9999" );
} );
function Check_Password() {
    if (document.getElementById( "pass" ).value == document.getElementById( "passP" ).value && document.getElementById("pass").value != "") {
        document.getElementById( "RegBut" ).removeAttribute( "disabled" );
    }
    else {
        document.getElementById( "wrongPw" ).style.display = "block; !important";
    }
}
