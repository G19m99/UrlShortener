import React from "react";
import lockIcon from "@/assets/lockIcon.svg";
import UrlResponse from "./UrlResponse";

const Card = ({ req, shortUrl }) => {
    const [urlInput, setUrlInput] = React.useState("");

    return (
        <div className="flex flex-col justify-between w-full h-full bg-white text-primary rounded-lg p-4 shadow-lg sm:max-h-[324px] sm:max-w-[340px]">
            <div className="basis-3/12 text-center sm:text-start pt-8 sm:pt-0">
                <h1 className="text-2xl font-semibold">Shorten A URL</h1>
                <p className="text-primary/90 text-sm">Shorten a long URL in one click</p>
            </div>
            <div className="flex flex-col justify-between items-center basis-9/12">
                <div className="flex flex-col gap-4 w-full">
                    <div className="flex flex-col space-y-1">
                        <label className="font-medium text-sm">URL</label>
                        <input className="border rounded-md h-10 px-3 py-2" placeholder=" Paste URL here" onChange={(e) => setUrlInput(e.target.value)} />
                    </div>
                    <div className="flex flex-col space-y-1">
                        <label className="font-medium text-sm">Domain</label>
                        <span className="relative">
                            <img src={lockIcon} className="h-4 w-4 absolute top-1/2 translate-x-1/2 -translate-y-1/2" alt="lock icon" />
                            <input className="border rounded-md h-10 pl-8 pr-3 py-2 w-full" readOnly placeholder="https://localhost/"></input>
                        </span>
                    </div>
                </div>
                <div className="sm:hidden">
                    <UrlResponse shortUrl={shortUrl} />
                </div>
                <div className="flex justify-end w-full">
                    <button onClick={() => req(urlInput)} className="inline-flex items-center justify-center whitespace-nowrap rounded-md text-sm font-medium ring-offset-background transition-colors focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-ring focus-visible:ring-offset-2 disabled:pointer-events-none disabled:opacity-50 bg-primary text-primaryText hover:bg-primary/90 h-10 px-4 py-2">Shorten URL</button>
                </div>
            </div>
        </div>
    );
}

export default Card;

