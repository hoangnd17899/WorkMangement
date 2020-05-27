$(document).ready(function () {
    var index = new Index();  
})

class Index {
    constructor() {
        this.InitEvent();
    }

    InitEvent() {
        $(document).on('click', 'input.phase_finish', this.CheckPhase);
    }

    CheckPhase() {
        var orderNumber = $(this).attr('orderNumber');
        var previousPhase = $('input[orderNumber="' + (orderNumber - 1) + '"]');
        if (previousPhase.prop('checked') == false) {
            alert('Các pha trước chưa được hoàn thành!');
            $(this).prop('checked', false);
        }
    }
}