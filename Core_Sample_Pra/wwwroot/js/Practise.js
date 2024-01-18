$(function () {
    //bootstrap5.1 彈跳視窗 自定義

    $('#EditBook_BOOK_STATUS').change(function () {

        HomeCheckEditDrowdownList();

    });
    //查詢按鈕
    $('#Serach').click(function () {

        $('#' + searchformname).attr('action', UrlIndex);
        $('#' + searchformname).submit();
    });


});

function HomeCheckEditDrowdownList() {
    let status = $('#EditBook_BOOK_STATUS').val();
    let KEEPER = $('#EditBook_BOOK_KEEPER');
    switch (status) {
        case 'A'://可以借出
        case 'U':
            KEEPER.prop("selectedIndex", 0);//清空
            KEEPER.attr("readonly", "readonly")//唯讀
            KEEPER.prop("disabled", true);
            break;
        case 'B':
        case 'C':
            KEEPER.removeAttr("readonly");
            KEEPER.prop("disabled", false);
            break;
        default:
            break;

    }
}
function SetAddDialog() {
    $("#" + editformname)[0].reset();
    $("#AddData").removeAttr("onClick");
    $('#EditBookModalLabel').text("新增");
    $('.AddHide').hide();
    $("#AddData").attr("onclick", "IsDataEdit(false)");
}
//type="reset"只能執行一次
function Reset() {
    $("#" + searchformname).find(':input').each(function () {
        if ($(this).is('select')) {

            $(this).prop('selectedIndex', 0);
        } else {

            $(this).val("");
        }

    });


}
//檢查參數
function Checkparameter(Check) {
   
    let IDName = 'EditBook_';
    let selectdatate = Date.parse($(`#${IDName}BOOK_BOUGHT_DATE`).val())
    console.log(selectdatate);

    const basetime = new Date(1900, 1, 1)
    let message = "";
    if ($(`#${IDName}BOOK_NAME`).val().trim() == '') {
        message += "請輸入書名\n"
    }
    if ($(`#${IDName}BOOK_AUTHOR`).val().trim() == '') {
        message += "輸入作者\n"
    }
    //出版商
    if ($(`#${IDName}BOOK_PUBLISHER`).val().trim() == '') {
        message += "輸入出版商\n"
    }

    if ($(`#${IDName}BOOK_NOTE`).val().trim() == '') {
        message += "請輸入內容簡介\n"
    }
    if (selectdatate <= basetime || isNaN(selectdatate)) {
        message += "請輸入購書日期\n"
    }
    if ($(`#${IDName}BOOK_CLASS_ID option:selected`).val().trim() == '') {
        message += "請選擇圖書類別\n"
    }

    if (Check) {
        let BOOK_STATUS = $(`#${IDName}BOOK_STATUS option:selected`).val().trim();
        let KEEPER = $(`#${IDName}BOOK_KEEPER option:selected`).val().trim();
        if (BOOK_STATUS == '') {
            message += "請選擇借閱狀態\n"
        }
        else if (BOOK_STATUS == "B" || BOOK_STATUS == "C") {
            if ($(`#${IDName}BOOK_KEEPER option:selected`).val().trim() == '') {
                message += "請選擇借閱人\n"
            }
        }

    }

    return message;

}
//EditView的按鈕 資料新增false或者編輯true
function IsDataEdit(Check) {
    let button = $(this);
    button.prop('disabled', true);
    let message = Checkparameter(Check);

    if (message == '') {
        if (Check) {
            EditAjax();
        }
        else {
            AddAjax();
        }
    }
    else {
        alert(message);
    }

    button.prop('disabled', false);
}
//修改按鈕 進入修改畫面
function Editshow(id) {
    $.ajax({
        url: UrlGetEditData,
        dataType: "json",
        method: "Get",
        data: { book_id: id }
        ,
        success: function (data) {
            console.log(data);
            let IDName = 'EditBook_';
            $(`#${IDName}BOOK_ID`).val(data.booK_ID);
            $(`#${IDName}BOOK_NAME`).val(data.booK_NAME);
            $(`#${IDName}BOOK_AUTHOR`).val(data.booK_AUTHOR);
            $(`#${IDName}BOOK_PUBLISHER`).val(data.booK_PUBLISHER);
            $(`#${IDName}BOOK_NOTE`).val(data.booK_NOTE);
            $(`#${IDName}BOOK_BOUGHT_DATE`).val((data.booK_BOUGHT_DATE).substring(0, 10));
            $(`#${IDName}BOOK_CLASS_ID`).val(data.booK_CLASS_ID);
            $(`#${IDName}BOOK_STATUS`).val(data.booK_STATUS);
            $(`#${IDName}BOOK_KEEPER`).val(data.booK_KEEPER);
            $("#AddData").removeAttr("onClick");
            $('#EditBookModalLabel').text("編輯");
            HomeCheckEditDrowdownList();
            $('.AddHide').show();
            $("#AddData").attr("onClick", "IsDataEdit(true)");
        },
        error: function (xhr, status, error) {

            console.error(error);
        }
    });

}
//除按鈕 刪除該筆資料
function Delete(id, Name) {
    var yes = confirm(`你確定刪除【${Name}】嗎？`);
    if (yes) {
        $.ajax({
            url: UrlDelete,
            dataType: "json",
            method: "Delete",
            data: { book_id: id }
            ,
            success: function (data) {
                //為什麼我設定Model 參數是大寫到這裡變成小寫
                if (data) {
                    alert("刪除成功");
                    window.location.href = UrlIndex;
                }
                else {
                    alert("刪除失敗")
                }
            },
            error: function (xhr, status, error) {
                console.error(error);
            }
        })
    }
}
function GetBookDetail(book_id) {
    let tbodyContainer = $('#BookDetailContainer');

    $.ajax({
        url: UrlGetDetail,
        method: "Get",
        data: { book_id: book_id },
        async: false,
        success: function (response) {
            tbodyContainer.empty();
            console.log(response);
            let row = $("<tr></tr>");
            for (let key in response) {
                row = $("<tr></tr>");
                if (response.hasOwnProperty(key)) {
                    let cellData = response[key];
                    let cell = $(`<td>${key}</td><td>${cellData}</td>`);
                    row.append(cell);
                }
                tbodyContainer.append(row)
            }

        },
        error: function (xhr, status, error) {
            console.error(error);
        }
    })

}
function GetHistory(book_id) {
    let tbodyContainer = $('#listContainer');

    $.ajax({
        url: UrlGetHistory,
        method: "Get",
        data: { book_id: book_id },
        async: false,
        success: function (response) {
            tbodyContainer.empty();
            console.log(response);
            for (let i = 0; i < response.length; i++) {
                let rowData = response[i];
                let row = $("<tr></tr>");

                for (let key in rowData) {
                    console.log(key);
                    if (rowData.hasOwnProperty(key)) {
                        let cellData = rowData[key];
                        let cell = $(`<td>${cellData}</td>`);
                        row.append(cell);
                    }
                }

                tbodyContainer.append(row);
            }
        },
        error: function (xhr, status, error) {
            console.error(error);
        }
    })

}

function EditAjax() {
    $.ajax({
        url: UrlEdit,
        dataType: "json",
        method: "Put",
        data: $('#' + editformname).serialize(),
        async: false,
        success: function (data) {
            alert(data.message);
            if (data.status) {

                window.location.href = UrlIndex;
            }
        },
        error: function (xhr, status, error) {
            console.error(error);
        }
    });
}
function AddAjax() {

    $.ajax({
        url: UrlInsert,
        dataType: "json",
        method: "Post",
        data: $('#' + editformname).serialize(),
        async: false,
        success: function (data) {
            alert(data.message);
            if (data.status) {
                window.location.href = UrlIndex;
            }
        },
        error: function (xhr, status, error) {
            console.error(error);
        }
    });
}