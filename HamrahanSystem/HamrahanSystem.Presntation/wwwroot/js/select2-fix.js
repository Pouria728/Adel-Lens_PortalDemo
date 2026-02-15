(function ($) {
    if (!$.fn.select2 || !$.fn.select2.defaults) {
        return;
    }

    $.fn.select2.defaults.set('dropdownAutoWidth', false);
    $.fn.select2.defaults.set('width', '100%');

    function syncDropdown($select) {
        var instance = $select.data('select2');
        if (!instance || !instance.$container || !instance.$dropdown) {
            return;
        }

        var $container = instance.$container;
        var $dropdown = instance.$dropdown;
        var width = $container.outerWidth();
        if (width) {
            $dropdown.css('width', width + 'px');
        }

        var $parent = instance.$dropdownParent || $(document.body);
        if ($parent.length && $parent[0] === document.body) {
            var offset = $container.offset();
            if (offset) {
                $dropdown.parent().css({
                    left: offset.left,
                    top: offset.top + $container.outerHeight()
                });
            }
        }
    }

    $(document).on('select2:open', function (e) {
        syncDropdown($(e.target));
    });

    $(window).on('resize', function () {
        $('.select2-hidden-accessible').each(function () {
            var $select = $(this);
            var instance = $select.data('select2');
            if (instance && instance.isOpen && instance.isOpen()) {
                syncDropdown($select);
            }
        });
    });
})(jQuery);
