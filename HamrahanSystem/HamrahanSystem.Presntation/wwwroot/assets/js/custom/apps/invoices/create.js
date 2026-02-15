"use strict";
var KTAppInvoicesCreate = (function () {
    var e,
        d = 0,
        a = function () {
            if (0 === e.querySelectorAll('[data-kt-element="items"] [data-kt-element="item"]').length) {
                var t = e.querySelector('[data-kt-element="empty-template"] tr').cloneNode(!0);
         
                e.querySelector('[data-kt-element="items"] tbody').appendChild(t);
            } else KTUtil.remove(e.querySelector('[data-kt-element="items"] [data-kt-element="empty"]'));
            
        },
        b = function () {
            var counter = e.querySelectorAll('[data-kt-element="items"] [data-kt-element="item"]').length;
            var items = e.querySelectorAll('[data-kt-element="items"] [data-kt-element="item"]')
            for (let m = 0; m < counter; m++) {
                var changer = items[m].getAttribute("data_kt_counter")
                items[m].querySelector('[data_kt_element="rowno"]').textContent = m + 1;
                for (let i = 0; i < items[m].querySelectorAll('[id]').length; i++) {
                    if (items[m].querySelectorAll('[id]')[i].id != undefined)
                        items[m].querySelectorAll('[id]')[i].id = items[m].querySelectorAll('[id]')[i].id.replace(changer, m)
                }
                for (let i = 0; i < items[m].querySelectorAll('[name]').length; i++) {
                    if (items[m].querySelectorAll('[name]')[i].name != undefined)
                        items[m].querySelectorAll('[name]')[i].name = items[m].querySelectorAll('[name]')[i].name.replace(changer, m)
                }
                items[m].setAttribute("data_kt_counter", m);
            }
            d = counter + 1;
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
            document.querySelector("#kt_invoice_form").querySelector('[data-kt-element="quantity"]').value = 1;
            e = document.querySelector("#kt_invoice_form");
            var setSelectValue = function (selector, value, text, triggerSelect) {
                var el = e.querySelector(selector);
                if (!el) {
                    return;
                }
                if (value === undefined || value === null || value === '') {
                    $(el).val(null).trigger('change');
                    return;
                }
                if (text) {
                    var existing = Array.from(el.options).find(function (opt) {
                        return opt.value == value;
                    });
                    if (!existing) {
                        el.add(new Option(text, value, true, true));
                    } else if (existing.text !== text) {
                        existing.text = text;
                    }
                }
                $(el).val(value);
                if (triggerSelect) {
                    $(el).trigger('select2:select');
                }
                $(el).trigger('change');
            };
            var findText = function (list, id) {
                if (!Array.isArray(list)) {
                    return '';
                }
                var item = list.find(function (x) { return x.id == id; });
                return item ? item.text : '';
            };
            var setSelectDataValue = function (selector, data, value, text) {
                var el = e.querySelector(selector);
                if (!el) {
                    return;
                }
                var list = Array.isArray(data) ? data : [];
                $(el).empty();
                $(el).select2({ data: list });
                if (value === undefined || value === null || value === '') {
                    $(el).val(null).trigger('change');
                    return;
                }
                var label = text || findText(list, value);
                if (label) {
                    var existing = Array.from(el.options).find(function (opt) {
                        return opt.value == value;
                    });
                    if (!existing) {
                        el.add(new Option(label, value, true, true));
                    } else if (existing.text !== label) {
                        existing.text = label;
                    }
                }
                $(el).val(value).trigger('change');
            };
            e.querySelector(' [data-kt-element="add-item"]').addEventListener("click", function (n) {

                var definecustomer = e.querySelector('[data-kt-element="definecustomer"]');
                if (definecustomer.value == undefined || definecustomer.value == "") {
                    toastr.error('مشتری را انتخاب نمایید ', '');
                    return;
                }

                var item = e.querySelector('[data-kt-element="defineobject"]')

                if (item.value == undefined || item.value == "") {
                    toastr.error('کالایی برای اضافه کردن انتخاب نکرده اید ', '');
                    return;
                }



                n.preventDefault();
                //var d = e.querySelectorAll('[data-kt-element="items"] [data-kt-element="item"]').length;

                var l = e.querySelector('[data-kt-element="item-template"] tr').cloneNode(!0);
                l.setAttribute("data_kt_counter", d);
                l.querySelector('[data_kt_element="rowno"]').textContent = d + 1;
                for (let i = 0; i < l.querySelectorAll('[id]').length; i++) {
                    if (l.querySelectorAll('[id]')[i].id != undefined)
                        l.querySelectorAll('[id]')[i].id = l.querySelectorAll('[id]')[i].id.replace('0', d)
                }
                for (let i = 0; i < l.querySelectorAll('[name]').length; i++) {
                    if (l.querySelectorAll('[name]')[i].name != undefined)
                        l.querySelectorAll('[name]')[i].name = l.querySelectorAll('[name]')[i].name.replace('0', d)
                }
                var selectedIndex = item.selectedIndex;
                var selectedOption = item.options[selectedIndex].text;
                l.querySelector('[data-kt-element="defineobjectid"]').value = item.value;
                l.querySelector('[data-kt-element="defineobjecttxt"]').textContent = selectedOption;

                item = e.querySelector('[data-kt-element="cyl"]');
                selectedIndex = item.selectedIndex;
                selectedOption = item.options[selectedIndex].text;
                l.querySelector('[data-kt-element="cylid"]').value = item.value;
                l.querySelector('[data-kt-element="cyltxt"]').textContent = selectedOption;


                item = e.querySelector('[data-kt-element="sph"]');
                selectedIndex = item.selectedIndex;
                selectedOption = item.options[selectedIndex].text;
                l.querySelector('[data-kt-element="sphid"]').value = item.value;
                l.querySelector('[data-kt-element="sphtxt"]').textContent = selectedOption;

                l.querySelector('[data-kt-element="defineobjecttxt"]').textContent = c(
                    l.querySelector('[data-kt-element="defineobjecttxt"]').textContent,
                    l.querySelector('[data-kt-element="sphtxt"]').textContent,
                    l.querySelector('[data-kt-element="cyltxt"]').textContent
                );

                item = e.querySelector('[data-kt-element="lensindex"]')
                l.querySelector('[data-kt-element="lensindexid"]').value = item.value;

                item = e.querySelector('[data-kt-element="lenstype"]')
                l.querySelector('[data-kt-element="lenstypeid"]').value = item.value;


                item = e.querySelector('[data-kt-element="brand"]')
                l.querySelector('[data-kt-element="brandid"]').value = item.value;

                item = e.querySelector('[data-kt-element="quantity"]')
                l.querySelector('[data-kt-element="quantityid"]').value = item.value == "" ? 1 : item.value;

                item = e.querySelector('[data-kt-element="needtools"]')
                l.querySelector('[data-kt-element="needtoolsid"]').checked = item.checked;

                item = e.querySelector('[data-kt-element="definecustomer"]')
                e.querySelector('[data-kt-element="definecustomerid"]').value = item.value;

                item = e.querySelector('[data-kt-element="storename"]')
                e.querySelector('[data-kt-element="storenametxt"]').value = item.textContent;

                //item = e.querySelector('[data-kt-element="consumer"]')
                //e.querySelector('[data-kt-element="consumertxt"]').value = item.textContent;

                d = d + 1;
                $('[data-kt-element="brand"]').val('').trigger('select2:select');
                $('[data-kt-element="brand"]').select2('val', '');
                $('[data-kt-element="brand"]').select2();
                e.querySelector('[data-kt-element="items"] tbody').appendChild(l), a(), b()
            }),
                KTUtil.on(e, '[data-kt-element="items"] [data-kt-element="edit-item"]', "click", function (n) {
                    n.preventDefault();
                    var row = this.closest('[data-kt-element="item"]');
                    if (!row) {
                        return;
                    }

                    var brandId = row.querySelector('[data-kt-element="brandid"]').value;
                    var lenstypeId = row.querySelector('[data-kt-element="lenstypeid"]').value;
                    var lensindexId = row.querySelector('[data-kt-element="lensindexid"]').value;
                    var sphId = row.querySelector('[data-kt-element="sphid"]').value;
                    var cylId = row.querySelector('[data-kt-element="cylid"]').value;
                    var defineObjectId = row.querySelector('[data-kt-element="defineobjectid"]').value;
                    var defineObjectText = row.querySelector('[data-kt-element="defineobjecttxt"]').textContent || '';
                    var sphText = row.querySelector('[data-kt-element="sphtxt"]').textContent || '';
                    var cylText = row.querySelector('[data-kt-element="cyltxt"]').textContent || '';

                    var listBrand = JSON.parse(sessionStorage.getItem('list_brand') || '[]');
                    var listLenstype = JSON.parse(sessionStorage.getItem('list_lenstype_r') || '[]');
                    var listLensindex = JSON.parse(sessionStorage.getItem('list_lensindex_r') || '[]');
                    var listSph = JSON.parse(sessionStorage.getItem('list_sph_r') || '[]');
                    var listCyl = JSON.parse(sessionStorage.getItem('list_cyl_r') || '[]');

                    window.__ktInvoiceEditRestore = true;

                    var normalizeId = function (value) {
                        if (value === undefined || value === null) {
                            return '';
                        }
                        var text = ('' + value).trim();
                        if (!text || text === '0') {
                            return '';
                        }
                        return text;
                    };

                    brandId = normalizeId(brandId);
                    lenstypeId = normalizeId(lenstypeId);
                    lensindexId = normalizeId(lensindexId);
                    sphId = normalizeId(sphId);
                    cylId = normalizeId(cylId);

                    if (!sphId && cylId) {
                        var cylItemForSph = listCyl.find(function (x) { return x.id == cylId; });
                        if (cylItemForSph && cylItemForSph.parentid) {
                            sphId = '' + cylItemForSph.parentid;
                        }
                    }
                    if (!lensindexId && sphId) {
                        var sphItemForLensindex = listSph.find(function (x) { return x.id == sphId; });
                        if (sphItemForLensindex && sphItemForLensindex.parentid) {
                            lensindexId = '' + sphItemForLensindex.parentid;
                        }
                    }

                    if (!lenstypeId && lensindexId) {
                        var lensindexItem = listLensindex.find(function (x) { return x.id == lensindexId; });
                        if (lensindexItem && lensindexItem.parentid) {
                            lenstypeId = '' + lensindexItem.parentid;
                        }
                    }
                    if (!brandId && lenstypeId) {
                        var lenstypeItem = listLenstype.find(function (x) { return x.id == lenstypeId; });
                        if (lenstypeItem && lenstypeItem.parentid) {
                            brandId = '' + lenstypeItem.parentid;
                        }
                    }

                    setSelectDataValue('[data-kt-element="brand"]', listBrand, brandId, findText(listBrand, brandId));

                    var lenstypeData = listLenstype.filter(function (x) { return x.parentid == brandId; });
                    setSelectDataValue('[data-kt-element="lenstype"]', lenstypeData, lenstypeId, findText(lenstypeData, lenstypeId) || findText(listLenstype, lenstypeId));

                    var lensindexData = listLensindex.filter(function (x) { return x.parentid == lenstypeId; });
                    setSelectDataValue('[data-kt-element="lensindex"]', lensindexData, lensindexId, findText(lensindexData, lensindexId) || findText(listLensindex, lensindexId));

                    var sphData = listSph.filter(function (x) { return x.parentid == lensindexId; });
                    setSelectDataValue('[data-kt-element="sph"]', sphData, sphId, findText(sphData, sphId) || sphText);

                    var cylData = listCyl.filter(function (x) { return x.parentid == sphId; });
                    setSelectDataValue('[data-kt-element="cyl"]', cylData, cylId, findText(cylData, cylId) || cylText);

                    var defineText = defineObjectText;
                    var defineId = defineObjectId;
                    var cylItem = listCyl.find(function (x) { return x.id == cylId; });
                    if (cylItem) {
                        if (!defineId && cylItem.defineobjectid) {
                            defineId = cylItem.defineobjectid;
                        }
                        if (!defineText && cylItem.nameobject) {
                            defineText = cylItem.nameobject;
                        }
                    }
                    var defineData = defineId ? [{ id: defineId, text: defineText || '' }] : [];
                    setSelectDataValue('[data-kt-element="defineobject"]', defineData, defineId, defineText || '');

                    setTimeout(function () {
                        window.__ktInvoiceEditRestore = false;
                    }, 50);

                    var quantity = e.querySelector('[data-kt-element="quantity"]');
                    if (quantity) {
                        quantity.value = row.querySelector('[data-kt-element="quantityid"]').value || 1;
                    }
                    var needtools = e.querySelector('[data-kt-element="needtools"]');
                    if (needtools) {
                        needtools.checked = row.querySelector('[data-kt-element="needtoolsid"]').checked;
                    }

                    KTUtil.remove(row);
                    a();
                    b();
                }),
                KTUtil.on(e, '[data-kt-element="items"] [data-kt-element="remove-item"]', "click", function (e) {
                    e.preventDefault(), KTUtil.remove(this.closest('[data-kt-element="item"]')), a(), b()
                }),
                KTUtil.on(e, '[data-kt-element="items"] [data-kt-element="quantity"], [data-kt-element="items"] [data-kt-element="price"],[data-kt-element="items"] [data-kt-element="discount"],[data-kt-element="items"] [data-kt-element="currency_price"],[data-kt-element="items"] [data-kt-element="currency_discount"],[data-kt-element="items"] [data-kt-element="sellerprofit"],[data-kt-element="items"] [data-kt-element="constructionamount"],[data-kt-element="items"] [data-kt-element="actionamount"]                    ', "change", function (e) {
                    e.preventDefault()
                }),
                KTUtil.on(e, ' [data-kt-element="submit-itemcyl"]', "click", function (n) {
                    n.preventDefault()
                    var t = [].slice.call(e.querySelectorAll('[data-kt-element="cylitems"] [data-kt-element="cylitem"]'))

                    t.map(function (r) {

                        //var val = r.querySelector('[data-kt-element="countlens"]');

                        var countlens= r.querySelector('[data-kt-element="countlens"]').value ;
                        var sphid = r.querySelector('[data-kt-element="sphidgrid"]').value;
                        var sphtitle = r.querySelector('[data-kt-element="sphtitlegrid"]').value;
                        var sphcylid = r.querySelector('[data-kt-element="sphcylidgrid"]').value;
                        var sphcyltitle = r.querySelector('[data-kt-element="sphcyltitlegrid"]').value;
                        var defineobjectid= r.querySelector('[data-kt-element="defineobjectidgrid"]').value;
                        var defineobjecttitle =r.querySelector('[data-kt-element="defineobjecttitlegrid"]').value ;
                        
                        


                        if (countlens != undefined && countlens != '' && countlens != '0') {


                            var l = e.querySelector('[data-kt-element="item-template"] tr').cloneNode(!0);
                            l.setAttribute("data_kt_counter", d);
                            l.querySelector('[data_kt_element="rowno"]').textContent = d + 1;
                            for (let i = 0; i < l.querySelectorAll('[id]').length; i++) {
                                if (l.querySelectorAll('[id]')[i].id != undefined)
                                    l.querySelectorAll('[id]')[i].id = l.querySelectorAll('[id]')[i].id.replace('0', d)
                            }
                            for (let i = 0; i < l.querySelectorAll('[name]').length; i++) {
                                if (l.querySelectorAll('[name]')[i].name != undefined)
                                    l.querySelectorAll('[name]')[i].name = l.querySelectorAll('[name]')[i].name.replace('0', d)
                            }

                            l.querySelector('[data-kt-element="defineobjectid"]').value = defineobjectid;
                            l.querySelector('[data-kt-element="defineobjecttxt"]').textContent = defineobjecttitle;

                            l.querySelector('[data-kt-element="cylid"]').value = sphcylid;
                            l.querySelector('[data-kt-element="cyltxt"]').textContent = sphcyltitle;


                            l.querySelector('[data-kt-element="sphid"]').value = sphid;
                            l.querySelector('[data-kt-element="sphtxt"]').textContent = sphtitle;

                            l.querySelector('[data-kt-element="defineobjecttxt"]').textContent = c(
                                l.querySelector('[data-kt-element="defineobjecttxt"]').textContent,
                                l.querySelector('[data-kt-element="sphtxt"]').textContent,
                                l.querySelector('[data-kt-element="cyltxt"]').textContent
                            );

                            var item = e.querySelector('[data-kt-element="lensindex"]')
                            l.querySelector('[data-kt-element="lensindexid"]').value = item.value;

                            item = e.querySelector('[data-kt-element="lenstype"]')
                            l.querySelector('[data-kt-element="lenstypeid"]').value = item.value;


                            item = e.querySelector('[data-kt-element="brand"]')
                            l.querySelector('[data-kt-element="brandid"]').value = item.value;

                            l.querySelector('[data-kt-element="quantityid"]').value = countlens;

                            item = e.querySelector('[data-kt-element="needtools"]')
                            l.querySelector('[data-kt-element="needtoolsid"]').checked = item.checked;

                            item = e.querySelector('[data-kt-element="definecustomer"]')
                            e.querySelector('[data-kt-element="definecustomerid"]').value = item.value;

                            item = e.querySelector('[data-kt-element="storename"]')
                            e.querySelector('[data-kt-element="storenametxt"]').value = item.textContent;


                            d = d + 1;
                            $('[data-kt-element="brand"]').val('').trigger('select2:select');
                            $('[data-kt-element="brand"]').select2('val', '');
                            $('[data-kt-element="brand"]').select2();
                            e.querySelector('[data-kt-element="items"] tbody').appendChild(l), a(), b()




                        }
                    }
                    );

                    $("#modal-Order").modal('hide');
                })
                ,
                KTUtil.on(e, ' [data-kt-element="add-itemcyl"]', "click", function (n) {

                    // e.querySelector('[data-kt-element="cylitems"]').innerHTML = '';
                    var definecustomer = e.querySelector('[data-kt-element="definecustomer"]');
                    if (definecustomer.value == undefined || definecustomer.value == "") {
                        toastr.error('مشتری را انتخاب نمایید ', '');
                        return;
                    }
                    
                    var item = e.querySelector('[data-kt-element="lensindex"]')

                    if (item.value == undefined || item.value == "") {
                        toastr.error('lens index را انتخاب نکرده اید ', '');
                        return;
                    }

                    const storedDataString = sessionStorage.getItem('list_sph_r');
                    let datasph = [];
                    let datasphid = [];
                    id = item.value;
                    if (storedDataString) {
                        datasph = JSON.parse(storedDataString);
                        datasph = datasph.filter(x => x.parentid == id);
                        datasphid = datasph.map(x => { return x.id; });
                    }
                    const storedDataString2 = sessionStorage.getItem('list_cyl_r');
                    let datacyl = [];
                    let datacyllist = [];
                    
                    if (storedDataString2) {
                        datacyl = JSON.parse(storedDataString2);
                        
                        datacyl = datacyl.filter(x => datasphid.includes(x.parentid));
                        datacyl.map(x => { return { value: x.cylid, text: x.text } }).forEach(item => {
                            if (datacyllist.filter(x => x.value == item.value).length == 0) {
                                datacyllist.push(item)
                            }
                        });
                    }
                    e.querySelector('[data-kt-element="cylitems"] table thead tr').innerHTML = '';
                    var l = e.querySelector('[data-kt-element="item-headercyl"] table thead tr th').cloneNode(!0);
                    l.innerHTML = 'SPH/CYL';
                    e.querySelector('[data-kt-element="cylitems"] table thead tr').appendChild(l.cloneNode(true));
                    for (let item in datacyllist) {
                        
                        l.innerHTML = datacyllist[item].text;
                        
                        e.querySelector('[data-kt-element="cylitems"] table thead tr').appendChild(l.cloneNode(true));
                    }


                    
                   

                    e.querySelector('[data-kt-element="cylitems"] table tbody').innerHTML = '';
                    for (let item in datasph) {
                        l = e.querySelector('[data-kt-element="item-headercyl"] table tbody tr').cloneNode(!0);
                        l.innerHTML = '';
                        var y = e.querySelector('[data-kt-element="item-headercyl"] table tbody td').cloneNode(!0);
                        y.setAttribute('data-kt-element', 'cylitemtitle');
                        y.innerHTML = datasph[item].text;
                        var sphid = datasph[item].id;
                        l.appendChild(y.cloneNode(true))
                        for (let itemcyl in datacyllist) {
                            var z = e.querySelector('[data-kt-element="item-headercyl"] table tbody td').cloneNode(!0);                            
                            var cylid = datacyllist[itemcyl].value;
                            //let data1 = datacyl.filter(x =>  x.parentid == sphid);
                            let data = datacyl.filter(x => x.cylid == cylid && x.parentid == sphid);
                            let datamaster = datacyl.filter(x => x.parentid == sphid);

                            
                            if (datamaster.length > 0) {
                                
                                if (data.length > 0) {
                                    z.querySelector('[data-kt-element="countlens"]').value = '';
                                    z.querySelector('[data-kt-element="sphidgrid"]').value = sphid;
                                    z.querySelector('[data-kt-element="sphtitlegrid"]').value = datasph[item].text;
                                    z.querySelector('[data-kt-element="sphcylidgrid"]').value = data[0].id;
                                    z.querySelector('[data-kt-element="sphcyltitlegrid"]').value = datacyllist[itemcyl].text;
                                    z.querySelector('[data-kt-element="defineobjectidgrid"]').value = data[0].defineobjectid;
                                    z.querySelector('[data-kt-element="defineobjecttitlegrid"]').value = data[0].nameobject;
                                    z.querySelector('[data-kt-element="countlens"]').setAttribute('placeholder', '');
                                    z.querySelector('[data-kt-element="labelcountlens"]').textContent = '';
                                    
                                    l.appendChild(z.cloneNode(true));
                                }
                                else {
                                    z.setAttribute('data-kt-element', 'cylitemtnull');
                                    z.innerHTML = ' ';
                                    l.appendChild(z.cloneNode(true));
                                }
                            }
                        }
                        e.querySelector('[data-kt-element="cylitems"] table tbody').appendChild(l.cloneNode(true));
                    }


                    
                    

                   
                    

                   // item = e.querySelector('[data-kt-element="lensindex"]')
                    //l.querySelector('[data-kt-element="lensindexid"]').value = item.value;


                   // e.querySelector('[data-kt-element="cylitems"]').appendChild(l),
                        $("#modal-Order").modal('show');
                })

        }
    };
})();
KTUtil.onDOMContentLoaded(function () {
    KTAppInvoicesCreate.init();
});
