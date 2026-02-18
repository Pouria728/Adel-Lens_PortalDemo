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
      
        },
        c = function (name, sph, cyl) {
            var raw = (name || '').toString();
            var result = raw.trim();
            if (!result) {
                return result;
            }

            function normalizeText(value) {
                return (value || '').toString().replace(/[\u200E\u200F]/g, '').trim();
            }

            function parseNumber(value) {
                var text = normalizeText(value).replace(/^\+/, '');
                if (!text) {
                    return null;
                }
                var num = Number(text);
                if (!isNaN(num)) {
                    return num;
                }
                return text;
            }

            function valuesEqual(left, right) {
                if (!left || !right) {
                    return false;
                }
                var leftValue = parseNumber(left);
                var rightValue = parseNumber(right);
                if (typeof leftValue === 'number' && typeof rightValue === 'number') {
                    return Math.abs(leftValue - rightValue) < 0.001;
                }
                return leftValue === rightValue;
            }

            var hasLrm = raw.indexOf('\u200E') === 0;
            var cleanedName = normalizeText(result);
            var parts = cleanedName.split(/\s+/);
            var sphText = normalizeText(sph);
            var cylText = normalizeText(cyl);

            if (parts.length >= 2 && sphText && cylText &&
                valuesEqual(parts[parts.length - 2], sphText) &&
                valuesEqual(parts[parts.length - 1], cylText)) {
                parts = parts.slice(0, -2);
            } else if (parts.length >= 1) {
                var last = parts[parts.length - 1];
                if ((sphText && valuesEqual(last, sphText)) || (cylText && valuesEqual(last, cylText))) {
                    parts = parts.slice(0, -1);
                }
            }

            var output = parts.join(' ').trim();
            if (!output) {
                return output;
            }
            return (hasLrm ? '\u200E' : '') + output;
        }

    return {
        init: function (n) {
            //document.querySelector("#kt_invoice_form").querySelector('[data-kt-element="quantity"]').value = 1;
            e = document.querySelector("#kt_invoice_form");
            var setSelectValue = function (selector, value) {
                var el = e.querySelector(selector);
                if (!el) {
                    return;
                }
                $(el).val(value).trigger('change');
            };
            e.addEventListener("keydown", function (event) {
                if (event.key !== "Tab" || event.shiftKey) {
                    return;
                }
                if (!event.target || !event.target.matches('input[name="radioGroup"]')) {
                    return;
                }
                var checked = e.querySelector('input[name="radioGroup"]:checked');
                if (!checked || checked.value !== "1") {
                    return;
                }
                var rightSph = e.querySelector('[data-kt-element="sph1"]');
                if (!rightSph) {
                    return;
                }
                event.preventDefault();
                rightSph.focus();
            });
            e.querySelector(' [data-kt-element="add-item"]').addEventListener("click", function (n) {


                var side = e.querySelector('input[name="radioGroup"]:checked').value;



                var item = side == 3 || side == 1 ? e.querySelector('[data-kt-element="defineobject1"]') : e.querySelector('[data-kt-element="defineobject0"]');

                var itemr = side == 1 ? e.querySelector('[data-kt-element="defineobject0"]') : e.querySelector('[data-kt-element="defineobject1"]');
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

                item = side == 3 || side == 1 ? e.querySelector('[data-kt-element="cyl1"]') : e.querySelector('[data-kt-element="cyl0"]');
                selectedIndex = item.selectedIndex;
                selectedOption = item.options[selectedIndex].text;
                l.querySelector('[data-kt-element="cylid0"]').value = item.value;
                l.querySelector('[data-kt-element="cyltxt0"]').textContent = selectedOption;


                item = side == 3 || side == 1 ? e.querySelector('[data-kt-element="sph1"]') : e.querySelector('[data-kt-element="sph0"]');
                selectedIndex = item.selectedIndex;
                selectedOption = item.options[selectedIndex].text;
                l.querySelector('[data-kt-element="sphid0"]').value = item.value;
                l.querySelector('[data-kt-element="sphtxt0"]').textContent = selectedOption;

                l.querySelector('[data-kt-element="defineobjecttxt0"]').textContent = c(
                    l.querySelector('[data-kt-element="defineobjecttxt0"]').textContent,
                    l.querySelector('[data-kt-element="sphtxt0"]').textContent,
                    l.querySelector('[data-kt-element="cyltxt0"]').textContent
                );

                item = side == 3 || side == 1 ? e.querySelector('[data-kt-element="axis1"]') : e.querySelector('[data-kt-element="axis0"]');
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

                var storeEdit = e.querySelector('[data-kt-element="storeedit"]');
                var storeValue = storeEdit ? storeEdit.value : "";
                if (storeValue) {
                    l.querySelector('[data-kt-element="storetxt0"]').value = storeValue;
                    l.querySelector('[data-kt-element="store0"]').value = storeValue;
                    if (side == 1) {
                        l.querySelector('[data-kt-element="storetxt1"]').value = storeValue;
                        l.querySelector('[data-kt-element="store1"]').value = storeValue;
                    }
                }

                item = e.querySelector('[data-kt-element="definecustomer"]');
                e.querySelector('[data-kt-element="definecustomerid"]').value = item.value;

                item = e.querySelector('[data-kt-element="storename"]');
                if (item) {
                    e.querySelector('[data-kt-element="storenametxt"]').value = item.value;
                }

                

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

                    item = e.querySelector('[data-kt-element="cyl0"]');
                    selectedIndex = item.selectedIndex;
                    selectedOption = item.options[selectedIndex].text;
                    l.querySelector('[data-kt-element="cylid1"]').value = item.value;
                    l.querySelector('[data-kt-element="cyltxt1"]').textContent = selectedOption;


                    item = e.querySelector('[data-kt-element="sph0"]')
                    selectedIndex = item.selectedIndex;
                    selectedOption = item.options[selectedIndex].text;
                    l.querySelector('[data-kt-element="sphid1"]').value = item.value;
                    l.querySelector('[data-kt-element="sphtxt1"]').textContent = selectedOption;

                    l.querySelector('[data-kt-element="defineobjecttxt1"]').textContent = c(
                        l.querySelector('[data-kt-element="defineobjecttxt1"]').textContent,
                        l.querySelector('[data-kt-element="sphtxt1"]').textContent,
                        l.querySelector('[data-kt-element="cyltxt1"]').textContent
                    );

                    item = e.querySelector('[data-kt-element="axis0"]')
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
                    l.querySelectorAll('input[type="hidden"][name="TblLnsOrderItems[1].NeedTools"]').forEach(item => item.remove());
                    d = d + 1;
                }

                var consumerText0 = l.querySelector('[data-kt-element="consumertxt0"]');
                var consumerHidden0 = l.querySelector('[data-kt-element="consumer0"]');
                var consumerText1 = l.querySelector('[data-kt-element="consumertxt1"]');
                var consumerHidden1 = l.querySelector('[data-kt-element="consumer1"]');
                var storeText0 = l.querySelector('[data-kt-element="storetxt0"]');
                var storeHidden0 = l.querySelector('[data-kt-element="store0"]');
                var storeText1 = l.querySelector('[data-kt-element="storetxt1"]');
                var storeHidden1 = l.querySelector('[data-kt-element="store1"]');
                var frameSelect0 = l.querySelector('[data-kt-element="frametypetxt0"]');
                var frameHidden0 = l.querySelector('[data-kt-element="frametype0"]');
                var frameSelect1 = l.querySelector('[data-kt-element="frametypetxt1"]');
                var frameHidden1 = l.querySelector('[data-kt-element="frametype1"]');

                if (consumerText0) {
                    consumerText0.addEventListener('input', function () {
                        if (consumerHidden0) {
                            consumerHidden0.value = consumerText0.value;
                        }
                        if (side == 1) {
                            if (consumerText1) {
                                consumerText1.value = consumerText0.value;
                            }
                            if (consumerHidden1) {
                                consumerHidden1.value = consumerText0.value;
                            }
                        }
                    });
                }

                if (storeText0) {
                    storeText0.addEventListener('input', function () {
                        if (storeHidden0) {
                            storeHidden0.value = storeText0.value;
                        }
                        if (side == 1) {
                            if (storeText1) {
                                storeText1.value = storeText0.value;
                            }
                            if (storeHidden1) {
                                storeHidden1.value = storeText0.value;
                            }
                        }
                    });
                }

                if (frameSelect0) {
                    if (frameHidden0) {
                        frameHidden0.value = frameSelect0.value;
                    }
                    if (side == 1) {
                        if (frameSelect1) {
                            frameSelect1.value = frameSelect0.value;
                        }
                        if (frameHidden1) {
                            frameHidden1.value = frameSelect0.value;
                        }
                    }
                    frameSelect0.addEventListener('change', function () {
                        if (frameHidden0) {
                            frameHidden0.value = frameSelect0.value;
                        }
                        if (side == 1) {
                            if (frameSelect1) {
                                frameSelect1.value = frameSelect0.value;
                            }
                            if (frameHidden1) {
                                frameHidden1.value = frameSelect0.value;
                            }
                        }
                    });
                }

                if (frameSelect1) {
                    if (frameHidden1) {
                        frameHidden1.value = frameSelect1.value;
                    }
                    frameSelect1.addEventListener('change', function () {
                        if (frameHidden1) {
                            frameHidden1.value = frameSelect1.value;
                        }
                    });
                }

                if (side == 1) {
                    if (consumerText1) {
                        consumerText1.style.display = "none";
                    }
                    if (storeText1) {
                        storeText1.style.display = "none";
                    }
                    if (frameSelect1) {
                        frameSelect1.style.display = "none";
                    }

                    var needTools0 = l.querySelector('[data-kt-element="needtoolsid0"]');
                    var needTools1 = l.querySelector('[data-kt-element="needtoolsid1"]');
                    var needToolsBr = l.querySelector('[data-kt-element="needtoolsbr"]');

                    if (needTools1) {
                        needTools1.style.display = "none";
                    }
                    if (needToolsBr) {
                        needToolsBr.style.display = "none";
                    }
                    if (needTools0 && needTools1) {
                        needTools0.addEventListener('change', function () {
                            needTools1.checked = needTools0.checked;
                        });
                    }
                }

                var storeEditRow = e.querySelector('[data-kt-element="store-edit-row"]');
                if (storeEditRow) {
                    storeEditRow.style.display = "none";
                }
                if (storeEdit) {
                    storeEdit.value = "";
                }

                $('[data-kt-element="brand"]').val('').trigger('change');
                $('[data-kt-element="axis0"]').val('');
                $('[data-kt-element="axis1"]').val('');
                e.querySelector('[data-kt-element="items"] tbody').appendChild(l), a(), b()
            }),
                KTUtil.on(e, '[data-kt-element="items"] [data-kt-element="edit-item"]', "click", function (n) {
                    n.preventDefault();
                    var row = this.closest('[data-kt-element="item"]');
                    if (!row) {
                        return;
                    }

                    var hasBoth = row.querySelector('[data-kt-rel="true"]');
                    var side = 1;
                    if (!hasBoth) {
                        var isRightValue = row.querySelector('[data-kt-element="isright0"]');
                        side = isRightValue && isRightValue.value == "1" ? 3 : 2;
                    }

                    var radio = e.querySelector('input[name="radioGroup"][value="' + side + '"]');
                    if (radio) {
                        radio.checked = true;
                        $(radio).trigger('change');
                    }

                    setSelectValue('[data-kt-element="brand"]', row.querySelector('[data-kt-element="brandid0"]').value);
                    setSelectValue('[data-kt-element="lenstype"]', row.querySelector('[data-kt-element="lenstypeid0"]').value);
                    setSelectValue('[data-kt-element="lensindex"]', row.querySelector('[data-kt-element="lensindexid0"]').value);

                    var setEye = function (index, dataPrefix) {
                        setSelectValue('[data-kt-element="sph' + index + '"]', row.querySelector('[data-kt-element="sphid' + dataPrefix + '"]').value);
                        setSelectValue('[data-kt-element="cyl' + index + '"]', row.querySelector('[data-kt-element="cylid' + dataPrefix + '"]').value);
                        var axis = e.querySelector('[data-kt-element="axis' + index + '"]');
                        if (axis) {
                            axis.value = row.querySelector('[data-kt-element="axisid' + dataPrefix + '"]').value || "";
                        }
                        var defineObject = e.querySelector('[data-kt-element="defineobject' + index + '"]');
                        if (defineObject) {
                            $(defineObject).val(row.querySelector('[data-kt-element="defineobjectid' + dataPrefix + '"]').value).trigger('change');
                        }
                    };

                    if (side == 1) {
                        setEye("1", "0");
                        setEye("0", "1");
                    } else if (side == 3) {
                        setEye("1", "0");
                    } else {
                        setEye("0", "0");
                    }

                    var consumer = e.querySelector('[data-kt-element="consumer"]');
                    if (consumer) {
                        var consumerValue = row.querySelector('[data-kt-element="consumer0"]').value;
                        consumer.value = consumerValue;
                    }
                    var needtools = e.querySelector('[data-kt-element="needtools"]');
                    if (needtools) {
                        needtools.checked = row.querySelector('[data-kt-element="needtoolsid0"]').checked;
                    }

                    var storeEditRow = e.querySelector('[data-kt-element="store-edit-row"]');
                    var storeEdit = e.querySelector('[data-kt-element="storeedit"]');
                    if (storeEditRow) {
                        storeEditRow.style.display = "block";
                    }
                    if (storeEdit) {
                        storeEdit.value = row.querySelector('[data-kt-element="store0"]').value || "";
                    }

                    KTUtil.remove(row);
                    a();
                    b();
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
