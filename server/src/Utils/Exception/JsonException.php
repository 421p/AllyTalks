<?php

namespace AllyTalks\Utils\Exception;

use Symfony\Component\HttpKernel\Exception\HttpException;

class JsonException extends HttpException
{
    public function __construct(string $message, int $code = 500)
    {
        parent::__construct($code, json_encode(['what' => $message]));
    }
}