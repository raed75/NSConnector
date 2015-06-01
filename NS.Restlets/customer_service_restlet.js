// Get Record
function getRecord(datain) {

    // Define search filters
    var filters = new Array();

    filters[0] = new nlobjSearchFilter('entityid', null, 'is', datain.clientid);

    // Define search columns
    var columnNames = [
        'entityid',
        'firstname',
        'lastname',
        'custentitybirth_data',
        'email',
        'mobilephone',
        'homephone',
        'custentity_fac_fl',
        'isinactive',
        'subsidiary'
    ];
    var columns = new Array();

    for (var i = 0; i < columnNames.length; i++) {
        columns[i] = new nlobjSearchColumn(columnNames[i]);
    }

    // Execute the Customer search. You must specify the internal ID of
    // the record type you are searching against. Also, you will pass the values
    // defined in the filtersand columnsarrays.
    var searchResults = nlapiSearchRecord('customer', null, filters, columns);
    var customer_entity;

    if (searchResults.length > 0) {

        customer_entity = {};
        
        // get only the first search result
        var searchresult = searchResults[0];
     
        for (var i=0; i < columns.length;i++) {
            customer_entity[columns[i].getName()] = searchresult.getValue(columns[i]);
        }
       
        //Get addressbook sublist record if exists (use: getLineItemValue(group, name, linenum))
        // 1. load customer record
        // 2. get the no of addresses in the addressbook sublist
        // 3. if there are addresses get the first address in the list (line=1)
        // 4. extract address fields and add it to the customer addressbook entity.

        // get the internal id of the record
        var internal_record_id = searchresult.getId();
        customer_entity.internalid = internal_record_id;

        var customer = nlapiLoadRecord('customer', internal_record_id);
        var addressbook_records_count = customer.getLineItemCount('addressbook')
        if (addressbook_records_count > 0) {
            customer_entity['addressbook'] = {};
            var fieldnames = ['country', 'state', 'city', 'zip', 'addr1', 'addr2', 'addrphone','isresidential'];
            for (var i = 0; i < fieldnames.length ; i++) {
                var fieldname = fieldnames[i];
                customer_entity['addressbook'][fieldname] = customer.getLineItemValue('addressbook', fieldname, 1);
            }
        }
    }

    return customer_entity;
}



// Create a standard NetSuite record
function createRecord(datain) {
    var err = new Object();
    // Validate if mandatory record type is set in the request
    if (!datain.recordtype) {
        err.status = "failed";
        err.message = "missing recordtype";
        return err;
    }
    // create the customer record
    var record = nlapiCreateRecord(datain.recordtype);
    for (var fieldname in datain) {
        if (datain.hasOwnProperty(fieldname)) {
            if (fieldname != 'recordtype' && fieldname != 'id') {
                var value = datain[fieldname];
                if (value && typeof value != 'object') // ignore other type of parameters
                {
                    record.setFieldValue(fieldname, value);
                }
            }
        }
    }
    // add address fields if exists (Sub record)
    if (datain.addressbook) {

        record.selectNewLineItem('addressbook');
        for (var fieldname in datain.addressbook) {

            if (datain.addressbook.hasOwnProperty(fieldname)) {

                var value = datain.addressbook[fieldname]
                if (value && typeof value != 'object') // ignore other type of parameters
                {

                    record.setCurrentLineItemValue('addressbook', fieldname, value);
                }
            }
        }
        record.commitLineItem('addressbook');
    }

    var recordId = nlapiSubmitRecord(record, false, false);

    nlapiLogExecution('DEBUG', 'id=' + recordId);

    // return the entity id (Client ID) as load function only return the internal ID.
    var entityId = nlapiLookupField(datain.recordtype, recordId, 'entityid');
    // set the id to the entity id and pass it to the search function to get the correct record schema
    datain.clientid = entityId;

    var record = getRecord(datain);

    return record;
}