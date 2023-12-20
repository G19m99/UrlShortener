import React from "react";

const useCreateShortUrl = () => {
    const [shortUrl, setShortUrl] = React.useState("");
    const shortenRequest = async (inputData) => {
        const body = {
            url: inputData
        }
        const res = await fetch("https://localhost:7164/api/url-shorten", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(body)
        });
        if (res.ok) {
            const resObj = await res.json();
            setShortUrl(resObj.shortUrl);
        }
        return null;
    }
    return {shortenRequest, shortUrl};
}

export default useCreateShortUrl; 