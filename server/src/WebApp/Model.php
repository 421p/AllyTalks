<?php

namespace AllyTalks\WebApp;

use AllyTalks\ORM\DoctrineFactory;
use AllyTalks\ORM\Entities\User;

class Model
{
    private $em;

    public function __construct()
    {
        $this->em = DoctrineFactory::buildEntityManagerFromConfig();
    }

    public function getAllUsers()
    {
        return $this->em->createQueryBuilder()
            ->select('u')
            ->from(User::class, 'u')
            ->getQuery()
            ->getResult();
    }

    public function getUserByLogin(string $login) : User
    {
        return $this->em->createQueryBuilder()
            ->select('usr')
            ->from(User::class, 'usr')
            ->where('usr.login = :log')
            ->setParameter('log', $login)
            ->getQuery()->getOneOrNullResult();
    }
}
