<?php

namespace AllyTalks\Utils\Exception;

use Symfony\Component\HttpKernel\Exception\HttpException;

class JsonException extends HttpException
{
    public function __construct(string $message, int $code = 401)
    {
        parent::__construct(401, json_encode(['what' => $message]));
    }
}
