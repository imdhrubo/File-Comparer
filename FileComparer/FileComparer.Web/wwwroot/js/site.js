// Write your JavaScript code.
$(document).ready(function () {
    $(".nav-tabs a").click(function () {
        $(this).tab('show');
    });
    $(document).ajaxStart(function () {
        $.blockUI(blockUISettings);
    });
    $(document).ajaxComplete(function () {
        // Hide image container
        $.unblockUI();
    });

    $('#generate').on('click', generateReport);
    $("#checkRows").on('change', enableTabForRows);
    $("#checkRowsWithSelectedColumn").on('change', enableTabForSelectedColumns);
    $("#checkGroups").on('change', enableTabForGroups);
    $("#checkDistribution").on('change', enableTabForDistribution);
    $('#standardFile').on('change', uploadStandard);
    $('#comparerFile').on('change', uploadComparer);
    $("#upload").click(function (e) {
        if ($(this).hasClass("reset")) {
            resetFiles(e);
        } else {
            uploadFiles(e);
        }
    });
});

var blockUISettings = {
    message: '<img width="50px" height="50px" src="/images/cloading.gif"/>',
    css: {
        border: 'none',
        backgroundColor: 'transparent',
        padding: '25px',
        cursor: 'wait'
    }
}

var toasterObject = {
    "closeButton": true,
    "debug": false,
    "newestOnTop": false,
    "progressBar": false,
    "positionClass": "toast-bottom-right",
    "preventDuplicates": false,
    "onclick": null,
    "showDuration": "300",
    "hideDuration": "1000",
    "timeOut": "5000",
    "extendedTimeOut": "1000",
    "showEasing": "swing",
    "hideEasing": "linear",
    "showMethod": "fadeIn",
    "hideMethod": "fadeOut"
}

files = new Array;
var standardFile;
var comparerFile;
var commonUniqueColumns = [];
var uniqueKeys = [];
var selectedColumns = [];
var selectedGroups = [];
var selectedDistributions = [];

function uploadSuccessNotification(message) {
    toastr.options = toasterObject;
    toastr["success"](message)
}

function uploadFailedNotification(message) {
    toastr.options = toasterObject;
    toastr["error"](message)
}

function validationNotification(message) {
    toastr.options = toasterObject;
    toastr["warning"](message)
}

function selectionValidation(message) {
    toastr.options = toasterObject;
    toastr["info"](message)
}

function areUniqueAndSelectedColumnsUnique() {
    var newUnique = [...uniqueKeys];
    var newSelected = [...selectedColumns];
    newUnique.sort(compareFunction);
    newSelected.sort(compareFunction);
    for (var i = 0; i != newUnique.length; i++) {
        var intersection = newUnique[i].Columns.filter(x => newSelected[i].Columns.includes(x));
        if (intersection.length > 0) {
            return false;
        }
    }
    return true;
}

function compareFunction(x, y) {
    return x.Sheet - y.Sheet;
}

function getSelectedSheetNames(data) {
    var sheets = [];
    for (var i = 0; i != data.length; i++) {
        sheets.push(data[i].Sheet);
    }
    return sheets;
}

function areSelectedColumnsValid() {
    var uniqueSheets = getSelectedSheetNames(uniqueKeys);
    var selectedSheets = getSelectedSheetNames(selectedColumns);
    if (uniqueSheets.sort().join(',') === selectedSheets.sort().join(',')) {
        return true;
    }
    else {
        return false;
    }
}

function enableTabForRows() {
    var ischecked = $(this).is(':checked');
    if (ischecked) {
        $("#rowTab").removeClass('disabled');
    }
    else {
        if (!$("#checkRowsWithSelectedColumn").is(':checked')) {
            $('.nav-tabs a[href="#basic"]').tab('show');
            $("#rowTab").addClass('disabled');
        }
    }
}

function enableTabForSelectedColumns() {
    var ischecked = $(this).is(':checked');
    if (ischecked) {
        if (!$("#checkRows").is(':checked')) {
            $("#rowTab").removeClass('disabled');
        }
        $("#rowWithSelectedColTab").removeClass('disabled');
    }
    else {
        if ($('.nav-link.active').attr('id') == "rowWithSelectedColTab") {
            $('.nav-tabs a[href="#basic"]').tab('show');
        }
        if ($('.nav-link.active').attr('id') == "rowTab" && !$("#checkRows").is(':checked')) {
            $('.nav-tabs a[href="#basic"]').tab('show');
        }
        $("#rowWithSelectedColTab").addClass('disabled');
        if (!$("#checkRows").is(':checked')) {
            $("#rowTab").addClass('disabled');
        }
    }
}

function enableTabForGroups() {
    var ischecked = $(this).is(':checked');
    if (ischecked) {
        $("#groupTab").removeClass('disabled');
    }
    else {
        if (!$("#checkGroups").is(':checked')) {
            if ($('.nav-link.active').attr('id') == "groupTab") {
                $('.nav-tabs a[href="#basic"]').tab('show');
            }
            $("#groupTab").addClass('disabled');
        }
    }
}

function enableTabForDistribution() {
    var ischecked = $(this).is(':checked');
    if (ischecked) {
        $("#distributionTab").removeClass('disabled');
    }
    else {
        if (!$("#checkDistribution").is(':checked')) {
            if ($('.nav-link.active').attr('id') == "distributionTab") {
                $('.nav-tabs a[href="#basic"]').tab('show');
            }
            $("#distributionTab").addClass('disabled');
        }
    }
}

function uploadStandard(event) {
    standardFile = event.target.files[0];
    var fileName = $(this).val().split("\\").pop();
    $(this).next('.custom-file-label').html(fileName);
}

function uploadComparer(event) {
    comparerFile = event.target.files[0];
    var fileName = $(this).val().split("\\").pop();
    $(this).next('.custom-file-label').html(fileName);
}

function resetFiles(event) {
    event.stopPropagation();
    event.preventDefault();
    setCurrentActiveTab();
    $("#upload").removeClass('reset');
    $("#upload").html('Upload');
    $("#standardFile").prop('disabled', false);
    $("#comparerFile").prop('disabled', false);
}

function uploadFiles(event) {

    event.stopPropagation();
    event.preventDefault();
    files[0] = standardFile;
    if ($('#comparerFile').val()) {
        files[1] = comparerFile;
    }

    var data = new FormData();
    for (var i = 0; i != files.length; i++) {
        data.append("files", files[i]);
    }
    if (!$('#standardFile').val()) {
        validationNotification("You must select a Standard Excel File");
    }
    else {
        $('#checkGroups').prop("checked", false);
        $('#checkDistribution').prop("checked", false);
        $("#groupTab").addClass('disabled');
        $("#distributionTab").addClass('disabled');
        $.ajax({
            url: 'api/FileComparer/UploadFiles',
            type: 'POST',
            data: data,
            cache: false,
            dataType: 'json',
            contentType: false,
            processData: false,
            success: function (result) {
                commonUniqueColumns = result;
                var $select = $('#uniqueCol');
                var $selectedColumns = $("#selectedCol");
                var $groupColumns = $("#groupCol");
                var $distributionColumns = $("#distributionCol");
                $select.empty();
                $selectedColumns.empty();
                $groupColumns.empty();
                $distributionColumns.empty();
                $(".selectpickerUnique").selectpicker();
                $(".selectpickerCol").selectpicker();
                $(".selectpickerGroup").selectpicker();
                $(".selectpickerDistribution").selectpicker();
                for (var i = 0; i != commonUniqueColumns.length; i++) {
                    var group1 = $('<optgroup label="' + commonUniqueColumns[i].sheet + '" />');
                    var group2 = $('<optgroup label="' + commonUniqueColumns[i].sheet + '" />');
                    var group3 = $('<optgroup label="' + commonUniqueColumns[i].sheet + '" />');
                    var group4 = $('<optgroup label="' + commonUniqueColumns[i].sheet + '" />');
                    for (var j = 0; j != commonUniqueColumns[i].columns.length; j++) {
                        $('<option value="' + commonUniqueColumns[i].columns[j] + '" data-tokens="' + commonUniqueColumns[i].sheet + ' ' + commonUniqueColumns[i].columns[j] + '"/>').html(commonUniqueColumns[i].columns[j]).appendTo(group1);
                        $('<option value="' + commonUniqueColumns[i].columns[j] + '" data-tokens="' + commonUniqueColumns[i].sheet + ' ' + commonUniqueColumns[i].columns[j] + '"/>').html(commonUniqueColumns[i].columns[j]).appendTo(group2);
                        $('<option value="' + commonUniqueColumns[i].columns[j] + '" data-tokens="' + commonUniqueColumns[i].sheet + ' ' + commonUniqueColumns[i].columns[j] + '"/>').html(commonUniqueColumns[i].columns[j]).appendTo(group3);
                        $('<option value="' + commonUniqueColumns[i].columns[j] + '" data-tokens="' + commonUniqueColumns[i].sheet + ' ' + commonUniqueColumns[i].columns[j] + '"/>').html(commonUniqueColumns[i].columns[j]).appendTo(group4);
                    }
                    group1.appendTo($select);
                    group2.appendTo($selectedColumns);
                    group3.appendTo($groupColumns);
                    group4.appendTo($distributionColumns);
                }
                $('.selectpickerUnique').selectpicker('refresh');
                $('.selectpickerCol').selectpicker('refresh');
                $('.selectpickerGroup').selectpicker('refresh');
                $('.selectpickerDistribution').selectpicker('refresh');
                uploadSuccessNotification("File was Uploaded successfully!");
                if (!$('#comparerFile').val()) {
                    $('#checkRows').attr("disabled", true);
                    $("#checkRowsLabel").addClass('disable');
                    $('#checkRowsWithSelectedColumn').attr("disabled", true);
                    $("#checkRowsWithSelectedColumnLabel").addClass('disable');
                }
                else {
                    $('#checkRows').attr("disabled", false);
                    $("#checkRowsLabel").removeClass('disable');
                    $('#checkRowsWithSelectedColumn').attr("disabled", false);
                    $("#checkRowsWithSelectedColumnLabel").removeClass('disable');
                }
                $("#standardFile").prop('disabled', true);
                $("#comparerFile").prop('disabled', true);
                $("#upload").html('Reset');
                $("#upload").addClass('reset');
                $("#card").attr('hidden', false);
            },
            error: function (error) {
                uploadFailedNotification("Something went wrong! Try again");
            }
        });
    }
}

function getSelectedUniqueColumns() {
    uniqueKeys.length = 0;
    var theSelect = document.getElementById("uniqueCol");
    var optgroups = theSelect.getElementsByTagName('optgroup');
    for (var i = 0; i < optgroups.length; i++) {
        var sheet = optgroups[i].getAttribute('label');
        var columns = [];
        var options = optgroups[i].getElementsByTagName('option');

        for (var j = 0; j < options.length; j++) {
            if (options[j].selected) { // check if options is selected
                columns.push(options[j].value);
            }
        }
        if (columns.length != 0) {
            var column = {
                Sheet: sheet,
                Columns: columns
            }
            uniqueKeys.push(column);
        }
    }
    return uniqueKeys;
}

function getSelectedColumns() {
    selectedColumns.length = 0;
    var theSelect = document.getElementById("selectedCol");
    var optgroups = theSelect.getElementsByTagName('optgroup');
    for (var i = 0; i < optgroups.length; i++) {
        var sheet = optgroups[i].getAttribute('label');
        var columns = [];
        var options = optgroups[i].getElementsByTagName('option');

        for (var j = 0; j < options.length; j++) {
            if (options[j].selected) { // check if options is selected
                columns.push(options[j].value);
            }
        }
        if (columns.length != 0) {
            var column = {
                Sheet: sheet,
                Columns: columns
            }
            selectedColumns.push(column);
        }
    }
    return selectedColumns;
}

function getSelectedGroupsColumns() {
    selectedGroups = [];
    var theSelect = document.getElementById("groupCol");
    var optgroups = theSelect.getElementsByTagName('optgroup');
    for (var i = 0; i < optgroups.length; i++) {
        var sheet = optgroups[i].getAttribute('label');
        var columns = [];
        var options = optgroups[i].getElementsByTagName('option');

        for (var j = 0; j < options.length; j++) {
            if (options[j].selected) { // check if options is selected
                columns.push(options[j].value);
            }
        }
        if (columns.length != 0) {
            var column = {
                Sheet: sheet,
                Columns: columns
            }
            selectedGroups.push(column);
        }
    }
    return selectedGroups;
}

function getSelectedDistributionColumns() {
    selectedDistributions = [];
    var theSelect = document.getElementById("distributionCol");
    var optgroups = theSelect.getElementsByTagName('optgroup');
    for (var i = 0; i < optgroups.length; i++) {
        var sheet = optgroups[i].getAttribute('label');
        var columns = [];
        var options = optgroups[i].getElementsByTagName('option');

        for (var j = 0; j < options.length; j++) {
            if (options[j].selected) { // check if options is selected
                columns.push(options[j].value);
            }
        }
        if (columns.length != 0) {
            var column = {
                Sheet: sheet,
                Columns: columns
            }
            selectedDistributions.push(column);
        }
    }
    return selectedDistributions;
}

function setCurrentActiveTab() {
    $("#card").attr('hidden', true);
    $('.nav-tabs a[href="#basic"]').tab('show');
    $('#checkRows').prop("checked", false);
    $('#checkRowsWithSelectedColumn').prop("checked", false);
    $('#checkGroups').prop("checked", false);
    $('#checkDistribution').prop("checked", false);
    $("#rowTab").addClass('disabled');
    $("#rowWithSelectedColTab").addClass('disabled');
    $("#groupTab").addClass('disabled');
    $("#distributionTab").addClass('disabled');
}

function generateReport(event) {
    event.stopPropagation();
    event.preventDefault();
    var config = {};
    if (!$("#checkColumns").is(':checked') && !$("#checkRows").is(':checked')
        && !$("#checkRowsWithSelectedColumn").is(':checked') && !$("#checkGroups").is(':checked')
        && !$("#checkDistribution").is(':checked')) {
        validationNotification("You must select atleast one configuraton");
        return;
    }
    if ($("#checkColumns").is(':checked')) {
        config.CheckColumns = true;
    }
    if ($("#checkRows").is(':checked')) {
        config.CheckRows = true;
        config.UniqueKeys = getSelectedUniqueColumns();
        if (uniqueKeys.length == 0) {
            selectionValidation("You must select atleast one unique key from row settings");
            return;
        }
    }
    if ($("#checkRowsWithSelectedColumn").is(':checked')) {
        config.CheckOnSelectedColumns = true;
        config.UniqueKeys = getSelectedUniqueColumns();
        config.SelectedColumns = getSelectedColumns();
        if (selectedColumns.length == 0) {
            selectionValidation("You must select atleast one Column from Column Settings");
            return;
        }
        if (!areSelectedColumnsValid()) {
            validationNotification("You must select atleast one unqiue key & one column from matching sheet in Row Settings & Column Settings tab");
            return;
        }
        if (!areUniqueAndSelectedColumnsUnique()) {
            validationNotification("Unique Key and Selected Columns cannot have common element");
            return;
        }
    }
    if ($("#checkGroups").is(':checked')) {
        config.CheckGroups = true;
        config.SelectedGroups = getSelectedGroupsColumns();
        if (selectedGroups.length == 0) {
            selectionValidation("You must select atleast One Column from Group Settings");
            return;
        }
    }
    if ($("#checkDistribution").is(':checked')) {
        config.CheckDistribution = true;
        config.SelectedDistributions = getSelectedDistributionColumns();
        if (selectedDistributions.length == 0) {
            selectionValidation("You must select atleast One Column from Distribution Settings");
            return;
        }
    }
    $.ajax({
        url: 'api/FileComparer/Generate',
        type: 'POST',
        data: JSON.stringify(config),
        cache: false,
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        processData: false,
        success: function (result) {
            uploadSuccessNotification("File Comparison was successful. Results are being downloaded!");
            window.location = 'api/FileComparer/Download';
            setCurrentActiveTab();
            if ($("#upload").hasClass('reset')) {
                resetFiles(event);
            }
        },
        error: function (error) {
            uploadFailedNotification("Something went wrong. Try Again");
            setCurrentActiveTab();
            if ($("#upload").hasClass('reset')) {
                resetFiles(event);
            }
        }
    });
}