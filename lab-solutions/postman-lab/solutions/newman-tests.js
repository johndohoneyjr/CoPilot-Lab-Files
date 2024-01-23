const newman = require('newman');

newman.run({
    collection: require('./path/to/your/postman-collection.json'),
    reporters: 'cli'
}, function (err) {
    if (err) {
        console.error('Newman run encountered an error:', err);
    }
    console.log('Newman run completed.');
});
