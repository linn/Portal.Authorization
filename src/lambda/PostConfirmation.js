import https from 'https';

export const handler = async (event, context) => {
    const sub = event.request.userAttributes.sub;

    if (!sub) {
        console.error("No sub found in userAttributes");
        return event;
    }

    const postData = JSON.stringify({ sub });

    const options = {
        hostname: 'www-sys.linn.co.uk',
        port: 443,
        path: '/portal-authorization/subjects',
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'Content-Length': postData.length,
            'X-shared-secret': '<secre>' // todo - inject somehow
        }
    };

    try {
        await makeRequest(options, postData);
        console.log(`Successfully notified API of new subject: ${sub}`);
    } catch (error) {
        console.error("Failed to call API:", error);
    }

    return event;
};

function makeRequest(options, postData) {
    return new Promise((resolve, reject) => {
        const req = https.request(options, (res) => {
            let data = '';

            res.on('data', (chunk) => data += chunk);
            res.on('end', () => {
                if (res.statusCode >= 200 && res.statusCode < 300) {
                    resolve(data);
                } else {
                    reject(`Status: ${res.statusCode}, Body: ${data}`);
                }
            });
        });

        req.on('error', reject);
        req.write(postData);
        req.end();
    });
}
