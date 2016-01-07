module.exports = function (config) {
    config.set({
        basePath: '../',

        files: [
            'Scripts/angular.js',
            'Scripts/angular-*.js',
            'app/controllers/**/*.js',
            'test/unit/**/*.js'
        ],
        exclude: ['**/*scenario.js'],
        autoWatch: true,
        frameworks: ['jasmine'],
        browsers: ['Chrome'],
        plugins: [
            'karma-junit-reporter',
            'karma-chrome-launcher',
            'karma-firefox-launcher',
            'karma-jasmine'
        ],

        junitReporter: {
            outputFile: 'test_out/unit.xml',
            suite: 'unit'
        }

    })
}
