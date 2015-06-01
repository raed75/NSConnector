function getCustomerByClientID(datain) {
    // Define search filters
    var filters = new Array();
    filters[0] = new nlobjSearchFilter('entityid', null, 'is', datain.clientid);

    // Define search columns
    var columnNames = [
        'entityid',
        'firstname',
        'lastname',
        'email',
        'address',
        'address1',
        'address2',
        'country',
        'zipcode',
        'city',
        'state',
        'mobilephone',
        'homephone',
        'custentity_fac_fl',
        'isinactive'
    ];
    var columns = new Array();

    for (var i = 0; i < columnNames.length; i++) {
        columns[i] = new nlobjSearchColumn(columnNames[i]);
    }

    // Execute the Customer search. You must specify the internal ID of
    // the record type you are searching against. Also, you will pass the values
    // defined in the filtersand columnsarrays.
    var searchResults = nlapiSearchRecord('customer', null, filters, columns);

    return searchResults;
}