// Make sure that your javscript code doesn't use any ES6 funcitonality such as let of const or arrow functions
// Otherwise Chutpah wont recognise the tests and show them in the test explorer.
// Make sure that the chutzpah.json file is set up and points to the correct source and test directory

describe('JS Test Validate Input',
    function () {
        it('Check for string length of 10', function () {
            var result = countValidCharacters("abcdefghij");
            expect(result).toEqual(true);
        });

        it('Check for string length of 40', function () {
            var result = countValidCharacters("abcdefghijabcdefghijabcdefghijabcdefghij");
            expect(result).toEqual(false);
        });
    });