import React from "react";


const UrlResponse = ({ shortUrl }) => {
    if (!shortUrl) return;
    const handleClick = async () => {
        await navigator.clipboard.writeText(shortUrl);
    }
    return (
        <div className="text-center text-primary flex flex-col sm:flex-row gap-2">
            <span className="font-medium">Shortened URL: </span>
            <p className="italic cursor-pointer" onClick={handleClick}>{shortUrl}</p>
        </div>
    )
}

export default UrlResponse;