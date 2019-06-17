const PROXY_CONFIG = [
    {
        context: [
            "**"
        ],
        target: "http://localhost:56658",
        secure: false
    }
];

module.exports = PROXY_CONFIG;
