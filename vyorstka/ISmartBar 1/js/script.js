function ISmartBar() {
    this.__init.call(this);
}
ISmartBar.prototype = {
    __init: function () {
        this.setData();
        this.__initEvents();
    },
    setData: function () {
        this.step_id = 0;

    },
    __initEvents: function () {
        let that = this;
        $(document).off('click', '.next-step').on('click', '.next-step', function () {
            let id = ++that.step_id;
            $('.step.active').removeClass('active');
            $('.step').eq(id).addClass('active');
            $('.dots .dot.active').removeClass('active');
            $('.dots .dot').eq(id).addClass('active');
        });
        $(document).off('click', '.dots .dot:not(.active)').on('click', '.dots .dot:not(.active)', function () {
            let id = $(this).attr('data-id');
            that.step_id = id;
            $('.step.active').removeClass('active');
            $('.step').eq(id).addClass('active');
            $('.dots .dot.active').removeClass('active');
            $('.dots .dot').eq(id).addClass('active');
        });
    }
};
let ISmartBarClass = new ISmartBar();