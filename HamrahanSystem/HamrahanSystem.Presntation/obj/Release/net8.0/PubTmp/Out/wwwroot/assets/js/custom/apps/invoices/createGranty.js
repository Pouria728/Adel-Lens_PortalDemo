"use strict";
var KTAppInvoicesCreateGranty = (function () {
    var e,
        d = 0,
        a = function () {
            if (0 === e.querySelectorAll('[data-kt-element="items"] [data-kt-element="item"]').length) {
                var t = e.querySelector('[data-kt-element="empty-template"] tr').cloneNode(!0);
                e.querySelector('[data-kt-element="items"] tbody').appendChild(t);
            } else
                KTUtil.remove(e.querySelector('[data-kt-element="items"] [data-kt-element="empty"]'));

        },
        b = function () {
            var counter = e.querySelectorAll('[data-kt-element="items"] [data-kt-element="item"]').length;
            var items = e.querySelectorAll('[data-kt-element="items"] [data-kt-element="item"]');
            var countitem = 0;

            for (let m = 0; m < counter; m++) {

                var changer = parseInt(items[m].getAttribute("data_kt_counter"));
                var rel = items[m].querySelectorAll('[data-kt-rel="true"]').length;
                items[m].querySelector('[data_kt_element="rowno"]').textContent = m + 1;
                for (let i = 0; i < items[m].querySelectorAll('[id][data-kt-rel="false"]').length; i++) {
                    
                    if (items[m].querySelectorAll('[id][data-kt-rel="false"]')[i].id != undefined) {
                        items[m].querySelectorAll('[id][data-kt-rel="false"]')[i].id = items[m].querySelectorAll('[id][data-kt-rel="false"]')[i].id.replace(changer, countitem)
                        if (rel > 0) {
                        
                            items[m].querySelectorAll('[id][data-kt-rel="true"]')[i].id = items[m].querySelectorAll('[id][data-kt-rel="true"]')[i].id.replace(changer + 1, countitem + 1);
                        }
                    }
                }
                for (let i = 0; i < items[m].querySelectorAll('[name][data-kt-rel="false"]').length; i++) {
                    if (items[m].querySelectorAll('[name][data-kt-rel="false"]')[i].name != undefined) {
                        items[m].querySelectorAll('[name][data-kt-rel="false"]')[i].name = items[m].querySelectorAll('[name][data-kt-rel="false"]')[i].name.replace(changer, countitem)
                        if (rel > 0) {
                            items[m].querySelectorAll('[name][data-kt-rel="true"]')[i].name = items[m].querySelectorAll('[name][data-kt-rel="true"]')[i].name.replace(changer + 1, countitem + 1);
                        }
                    }
                }

                items[m].setAttribute("data_kt_counter", countitem);
                if (rel > 0) {
                    items[m].querySelector('[data-kt-element="rownumber0"]').value = m;
                    items[m].querySelector('[data-kt-element="rownumber1"]').value = m;
                    countitem = countitem + 2;
                }
                else {
                    items[m].querySelector('[data-kt-element="rownumber0"]').value = m;
                    countitem = countitem + 1;
                }

            }
            d = countitem ;
      
        }

    return {
        init: function (n) {
            //document.querySelector("#kt_invoice_form").querySelector('[data-kt-element="quantity"]').value = 1;
            (e = document.querySelector("#kt_invoice_form")).querySelector(' [data-kt-element="add-item"]').addEventListener("click", function (n) {


                var side = e.querySelector('input[name="radioGroup"]:checked').value;



                var item = side == 3 ? e.querySelector('[data-kt-element="defineobject1"]') : e.querySelector('[data-kt-element="defineobject0"]');

                var itemr = e.querySelector('[data-kt-element="defineobject1"]');
                var definecustomer=e.querySelector('[data-kt-element="definecustomer"]');
                if  (definecustomer.value == undefined || definecustomer.value == "") {
                    toastr.error('مشتری را انتخاب نمایید ', '');
                    return;
                }

                if (side == 3 && (item.value == undefined || item.value == "" || itemr.value == undefined || itemr.value == "")) {
                    toastr.error('کالایی برای اضافه کردن انتخاب نکرده اید ', '');
                    return;
                }
                if (side != 3 && (item.value == undefined || item.value == "")) {
                    toastr.error('کالایی برای اضافه کردن انتخاب نکرده اید ', '');
                    return;
                }

                if (side == 3 && (itemr.value == undefined || itemr.value == "")) {
                    toastr.error('کالایی برای اضافه کردن انتخاب نکرده اید ', '');
                    return;
                }


                n.preventDefault();
                //var d = e.querySelectorAll('[data-kt-element="items"] [data-kt-element="item"]').length;

                var l = e.querySelector('[data-kt-element="item-template"] tr').cloneNode(!0);
                l.setAttribute("data_kt_counter", d);
                l.querySelector('[data_kt_element="rowno"]').textContent = d + 1;
                for (let i = 0; i < l.querySelectorAll('[id][data-kt-rel="false"]').length; i++) {

                    if (l.querySelectorAll('[id][data-kt-rel="false"] ')[i].id != undefined) {
                        l.querySelectorAll('[id][data-kt-rel="false"]')[i].id = l.querySelectorAll('[id][data-kt-rel="false"]')[i].id.replace('0', d)
                        if (side == 1) {
                            l.querySelectorAll('[name][data-kt-rel="true"]')[i].id = l.querySelectorAll('[name][data-kt-rel="true"]')[i].id.replace('1', d + 1);
                        }
                    }
                }
                for (let i = 0; i < l.querySelectorAll('[name][data-kt-rel="false"]').length; i++) {
                    if (l.querySelectorAll('[name][data-kt-rel="false"]')[i].name != undefined) {
                        l.querySelectorAll('[name][data-kt-rel="false"]')[i].name = l.querySelectorAll('[name][data-kt-rel="false"]')[i].name.replace('0', d);
                        if (side == 1) {
                            l.querySelectorAll('[name][data-kt-rel="true"]')[i].name = l.querySelectorAll('[name][data-kt-rel="true"]')[i].name.replace('1', d + 1);
                        }
                    }
                }
                var selectedIndex = item.selectedIndex;
                var selectedOption = item.options[selectedIndex].text;
                l.querySelector('[data-kt-element="defineobjectid0"]').value = item.value;
                l.querySelector('[data-kt-element="defineobjecttxt0"]').textContent = selectedOption;

                item = side == 3 ? e.querySelector('[data-kt-element="cyl1"]') : e.querySelector('[data-kt-element="cyl0"]');
                selectedIndex = item.selectedIndex;
                selectedOption = item.options[selectedIndex].text;
                l.querySelector('[data-kt-element="cylid0"]').value = item.value;
                l.querySelector('[data-kt-element="cyltxt0"]').textContent = selectedOption;


                item = side == 3 ? e.querySelector('[data-kt-element="sph1"]') : e.querySelector('[data-kt-element="sph0"]');
                selectedIndex = item.selectedIndex;
                selectedOption = item.options[selectedIndex].text;
                l.querySelector('[data-kt-element="sphid0"]').value = item.value;
                l.querySelector('[data-kt-element="sphtxt0"]').textContent = selectedOption;

                item = side == 3 ? e.querySelector('[data-kt-element="axis1"]') : e.querySelector('[data-kt-element="axis0"]');
                l.querySelector('[data-kt-element="axisid0"]').value = item.value;
                l.querySelector('[data-kt-element="axistxt0"]').textContent = item.value == "" ? "--" : item.value;






                item = e.querySelector('[data-kt-element="lensindex"]');
                l.querySelector('[data-kt-element="lensindexid0"]').value = item.value;

                item = e.querySelector('[data-kt-element="lenstype"]');
                l.querySelector('[data-kt-element="lenstypeid0"]').value = item.value;


                item = e.querySelector('[data-kt-element="brand"]');
                l.querySelector('[data-kt-element="brandid0"]').value = item.value;


                item = e.querySelector('[data-kt-element="needtools"]');
                l.querySelector('[data-kt-element="needtoolsid0"]').checked = item.checked;

                item = e.querySelector('[data-kt-element="consumer"]');
                l.querySelector('[data-kt-element="consumer0"]').value = item.value;
                l.querySelector('[data-kt-element="consumertxt0"]').value = item.value;

                item = e.querySelector('[data-kt-element="definecustomer"]');
                e.querySelector('[data-kt-element="definecustomerid"]').value = item.value;

                item = e.querySelector('[data-kt-element="storename"]');
                e.querySelector('[data-kt-element="storenametxt"]').value = item.value;

                

                if (side == 1) {
                    l.querySelector('[data-kt-element="isright1"]').value = 0;
                    l.querySelector('[data-kt-element="isright0"]').value = 1;

                    item = e.querySelector('[data-kt-element="brand"]');
                    l.querySelector('[data-kt-element="brandid1"]').value = item.value;


                    item = e.querySelector('[data-kt-element="lensindex"]');
                    l.querySelector('[data-kt-element="lensindexid1"]').value = item.value;

                    item = e.querySelector('[data-kt-element="lenstype"]');
                    l.querySelector('[data-kt-element="lenstypeid1"]').value = item.value;

                    item = e.querySelector('[data-kt-element="consumer"]');
                    l.querySelector('[data-kt-element="consumertxt1"]').value = item.value;
                    l.querySelector('[data-kt-element="consumer1"]').value = item.value;

                    var selectedIndex = itemr.selectedIndex;
                    var selectedOption = itemr.options[selectedIndex].text;
                    l.querySelector('[data-kt-element="defineobjectid1"]').value = itemr.value;
                    l.querySelector('[data-kt-element="defineobjecttxt1"]').textContent = selectedOption;

                    item = e.querySelector('[data-kt-element="cyl1"]');
                    selectedIndex = item.selectedIndex;
                    selectedOption = item.options[selectedIndex].text;
                    l.querySelector('[data-kt-element="cylid1"]').value = item.value;
                    l.querySelector('[data-kt-element="cyltxt1"]').textContent = selectedOption;


                    item = e.querySelector('[data-kt-element="sph1"]')
                    selectedIndex = item.selectedIndex;
                    selectedOption = item.options[selectedIndex].text;
                    l.querySelector('[data-kt-element="sphid1"]').value = item.value;
                    l.querySelector('[data-kt-element="sphtxt1"]').textContent = selectedOption;

                    item = e.querySelector('[data-kt-element="axis1"]')
                    l.querySelector('[data-kt-element="axisid1"]').value = item.value;
                    l.querySelector('[data-kt-element="axistxt1"]').textContent = item.value == "" ? "--" : item.value;;

                    item = e.querySelector('[data-kt-element="needtools"]')
                    l.querySelector('[data-kt-element="needtoolsid1"]').checked = item.checked;


                    l.querySelector('[data-kt-element="rownumber0"]').value = d;
                    l.querySelector('[data-kt-element="rownumber1"]').value = d;

                    d = d + 2;
                }
                else {
                    l.querySelector('[data-kt-element="isright0"]').value = side == 3 ? 1 : 0;
                    l.querySelector('[data-kt-element="isrighttxt0"]').textContent = side == 3 ? 'R' : 'L';
                    l.querySelectorAll('[data-kt-rel="true"]').forEach(item => item.remove());
                    l.querySelectorAll('[name="TblLnsOrderItems[1].NeedTools"]').forEach(item => item.remove());
                    d = d + 1;
                }


                $('[data-kt-element="brand"]').val('').trigger('select2:select');
                $('[data-kt-element="brand"]').select2('val', '');
                $('[data-kt-element="brand"]').select2();
                $('[data-kt-element="axis0"]').val('');
                $('[data-kt-element="axis1"]').val('');
                e.querySelector('[data-kt-element="items"] tbody').appendChild(l), a(), b()
            }),
                KTUtil.on(e, '[data-kt-element="items"] [data-kt-element="remove-item"]', "click", function (e) {
                    e.preventDefault(), KTUtil.remove(this.closest('[data-kt-element="item"]')), a(), b()
                }),
                KTUtil.on(e, '[data-kt-element="items"] [data-kt-element="quantity"], [data-kt-element="items"] [data-kt-element="price"],[data-kt-element="items"] [data-kt-element="discount"],[data-kt-element="items"] [data-kt-element="currency_price"],[data-kt-element="items"] [data-kt-element="currency_discount"],[data-kt-element="items"] [data-kt-element="sellerprofit"],[data-kt-element="items"] [data-kt-element="constructionamount"],[data-kt-element="items"] [data-kt-element="actionamount"]                    ', "change", function (e) {
                    e.preventDefault();
                    
                })

        },
    };
})();
KTUtil.onDOMContentLoaded(function () {
    KTAppInvoicesCreateGranty.init();
});
